using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using StockApplication.Common.Messages;

namespace StockApplication.Business.Messaging
{
    public class ProducerHandler
    {
        public async Task ProduceMessageAsync(string stockCode, CancellationToken cancellationToken)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync(KafkaTopics.GetStockInfo, new Message<Null, string> { Value = stockCode }, cancellationToken);
                    Console.WriteLine($"Message with value '{dr.Value}' delivered to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
