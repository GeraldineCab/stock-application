using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Identity;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Messages;
using StockApplication.Dto;
using StockApplication.Persistence.Entities;

namespace StockApplication.Business.Messaging
{
    public class ProducerHandler : IProducerHandler
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public ProducerHandler(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <inheritdoc />
        public async Task<MessageDto> ProduceMessageAsync(string message, CancellationToken cancellationToken, bool isDecoupledCall = false)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    await p.ProduceAsync(KafkaTopics.GetStockInfo, new Message<Null, string> { Value = message }, cancellationToken);
                    if (!isDecoupledCall)
                    {
                        var messageDto = new MessageDto()
                        {
                            Username = _userService.GetUsername(),
                            Text = message, 
                            Date = DateTime.Now
                        };
                        return messageDto;
                    }
                    return new MessageDto();
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    return new MessageDto();
                }
            }
        }
    }
}
