using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Messaging
{
    public class ProducerHandler : IProducerHandler
    {
        private readonly IMessageService _messageService;
        private readonly IMessageValidationService _messageValidationService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProducerHandler(IMessageService messageService, IMessageValidationService messageValidationService, IHttpContextAccessor contextAccessor)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _messageValidationService = messageValidationService ?? throw new ArgumentNullException(nameof(messageValidationService));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        /// <inheritdoc />
        public async Task<bool> ProduceMessageAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false, bool isDecoupledCall = false)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var (validationResult, _) = _messageValidationService.ValidateStockCode(stockCode);
                    if (commandNeeded && !string.IsNullOrEmpty(validationResult.ErrorMessage))
                    {
                       return false;
                    }
                    var dr = await p.ProduceAsync(KafkaTopics.GetStockInfo, new Message<Null, string> { Value = stockCode }, cancellationToken);
                    
                    if (!isDecoupledCall)
                    {
                        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var message = new MessageDto() { Text = stockCode, UserId = userId};
                        await _messageService.AddMessageAsync(message, cancellationToken);
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
            return true;
        }
    }
}
