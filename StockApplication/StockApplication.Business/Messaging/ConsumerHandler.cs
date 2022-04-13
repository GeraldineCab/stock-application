using System;
using System.Collections.Generic;
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
        private readonly IMessageValidationService _messageValidationService;
        private readonly IUserService _userService;

        public ConsumerHandler(IStockService stockService, IMessageValidationService messageValidationService, IUserService userService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _messageValidationService = messageValidationService ?? throw new ArgumentNullException(nameof(messageValidationService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <inheritdoc />
        public async Task<IList<MessageDto>> ConsumeMessageAsync(CancellationToken cancellationToken, bool isDecoupledCall = false)
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
                var watermarkOffsets = c.QueryWatermarkOffsets(tp, TimeSpan.FromMilliseconds(1000));

                if (watermarkOffsets.Low.Equals(Offset.Unset) || watermarkOffsets.High.Equals(Offset.Unset))
                {
                    c.Assign(tp);
                }
                else
                {
                    var lastOffset = new Offset(watermarkOffsets.High - 1);
                    var tpo = new TopicPartitionOffset(tp, lastOffset);
                    c.Assign(tpo);
                }

                try
                {
                    try
                    {
                        var cr = c.Consume(cancellationToken);
                        var messageValue = cr.Message.Value;
                        var messages = new List<MessageDto>();
                        string finalMessage;

                        if (isDecoupledCall)
                        {
                            finalMessage = await _stockService.GetStockClosePriceAsync(messageValue, cancellationToken);
                        }
                        else
                        {
                            var stock = _messageValidationService.GetStockCommand(messageValue);
                            if (string.IsNullOrEmpty(stock))
                            {
                                finalMessage = messageValue;
                            }
                            else
                            {
                                var stockMessage = await _stockService.GetStockClosePriceAsync(stock, cancellationToken);
                                messages.Add(new MessageDto() { Text = messageValue, Username = _userService.GetUsername() });
                                messages.Add(new MessageDto() { Text = stockMessage, Username = "Bot" });
                                return messages;
                            }
                        }
                        messages.Add(new MessageDto() { Text = finalMessage, Username = _userService.GetUsername() });
                        return messages;
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
            return new List<MessageDto>();
        }
    }
}
