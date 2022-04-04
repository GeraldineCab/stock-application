using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Messages;

namespace StockApplication.Business.Messaging
{
    public class ConsumerHandler : IConsumerHandler
    {
        private readonly IStockService _stockService;
        public ConsumerHandler(IStockService stockService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
        }

        public async Task<string> ConsumeMessageAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(KafkaTopics.GetStockInfo);

                try
                {
                    try
                    {
                        var cr = c.Consume(cancellationToken);
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                        return await _stockService.GetStockClosePriceAsync(cr.Message.Value, cancellationToken); ;
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occurred: {e.Error.Reason}");
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
