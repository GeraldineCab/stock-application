using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Dto;

namespace StockApplication.Business.Services
{
    public class HomeService : IHomeService
    {
        private readonly IProducerHandler _producerHandler;
        private readonly IConsumerHandler _consumerHandler;

        public HomeService(IProducerHandler producerHandler, IConsumerHandler consumerHandler)
        {
            _producerHandler = producerHandler ?? throw new ArgumentNullException(nameof(producerHandler));
            _consumerHandler = consumerHandler ?? throw new ArgumentNullException(nameof(consumerHandler));
        }

        /// <inheritdoc />
        public async Task<IList<MessageDto>> SendMessageAsync(string stockCode, CancellationToken cancellationToken = default, bool isDecoupledCall = false)
        {
            var canProduceMessage = await _producerHandler.ProduceMessageAsync(stockCode, cancellationToken, isDecoupledCall);
            if (canProduceMessage)
            {
                return await _consumerHandler.ConsumeMessageAsync(cancellationToken, isDecoupledCall);
            }
            return new List<MessageDto>();
        }
    }
}
