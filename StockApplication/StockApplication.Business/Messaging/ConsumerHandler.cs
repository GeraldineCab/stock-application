using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Messaging
{
    public class ConsumerHandler : IConsumerHandler
    {
        private readonly IStockService _stockService;
        private readonly IMessageService _messageService;
        private readonly IMessageValidationService _messageValidationService;

        public ConsumerHandler(IStockService stockService, IMessageService messageService, IMessageValidationService messageValidationService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _messageValidationService = messageValidationService ?? throw new ArgumentNullException(nameof(messageValidationService));
        }

        /// <inheritdoc />
        public async Task<string> ConsumeMessageAsync(CancellationToken cancellationToken, bool isDecoupledCall = false)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(KafkaTopics.GetStockInfo);
                // Assigns the consumer to the last offset in the partition
                var tp = new TopicPartition(KafkaTopics.GetStockInfo, new Partition(0));
                var watermarkOffsets = c.GetWatermarkOffsets(tp);
                var lastOffset = new Offset(watermarkOffsets.High - 1);
                var tpo = new TopicPartitionOffset(tp, lastOffset);
                c.Assign(tpo);

                try
                {
                    try
                    {
                        var cr = c.Consume(cancellationToken);
                        c.Commit();
                        var messageValue = cr.Message.Value;
                        var stock = _messageValidationService.GetStockCommand(messageValue);
                        string finalMessage;

                        if (isDecoupledCall)
                        {
                            finalMessage = await _stockService.GetStockClosePriceAsync(messageValue, cancellationToken);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(stock))
                            {
                                finalMessage = messageValue;
                            }
                            else
                            {
                                finalMessage = await _stockService.GetStockClosePriceAsync(messageValue, cancellationToken);
                            }
                            var message = new MessageDto() { Text = finalMessage, Username = "Bot" };
                            await _messageService.AddMessageAsync(message, cancellationToken);
                        }
                        return finalMessage;
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }

            return string.Empty;
        }
    }
}
