using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Common.Messages;

namespace StockApplication.Business.Messaging
{
    public class ProducerHandler : IProducerHandler
    {
        /// <inheritdoc />
        public async Task<bool> ProduceMessageAsync(string message, CancellationToken cancellationToken, bool isDecoupledCall = false)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    await p.ProduceAsync(KafkaTopics.GetStockInfo, new Message<Null, string> { Value = message }, cancellationToken);
                    return true;
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    return false;
                }
            }
        }
    }
}
