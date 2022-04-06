using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Messaging
{
    public class ConsumerHandler : IConsumerHandler
    {
        private readonly IStockService _stockService;
        private readonly IMessageService _messageService;

        public ConsumerHandler(IStockService stockService, IMessageService messageService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        public async Task<string> ConsumeMessageAsync(CancellationToken cancellationToken, bool commandNeeded = false, bool isDecoupledCall = false)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                var tp = new TopicPartition(KafkaTopics.GetStockInfo, Partition.Any);
                c.Assign(tp);
                var offset = c.Position(tp) - 1;
                var top = new TopicPartitionOffset(tp, offset);
                c.Seek(top);
                c.Subscribe(KafkaTopics.GetStockInfo);

                try
                {
                    try
                    {
                        var cr = c.Consume(cancellationToken);

                        var stockMessage = await _stockService.GetStockClosePriceAsync(cr.Message.Value,
                            commandNeeded: commandNeeded, cancellationToken: cancellationToken);

                        if (!isDecoupledCall)
                        {
                            var message = new MessageDto() { Text = stockMessage, UserId = "3" };
                            await _messageService.AddMessageAsync(message, cancellationToken);
                        }

                        return stockMessage;
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
