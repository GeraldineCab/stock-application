using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Messaging
{
    public class ProducerHandler : IProducerHandler
    {
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProducerHandler(IMessageService messageService, IHttpContextAccessor contextAccessor)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        /// <inheritdoc />
        public async Task ProduceMessageAsync(string message, CancellationToken cancellationToken, bool isDecoupledCall = false)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    await p.ProduceAsync(KafkaTopics.GetStockInfo, new Message<Null, string> { Value = message }, cancellationToken);
                    if (!isDecoupledCall)
                    {
                        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var messageDto = new MessageDto() { Text = message, UserId = userId};
                        await _messageService.AddMessageAsync(messageDto, cancellationToken);
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
