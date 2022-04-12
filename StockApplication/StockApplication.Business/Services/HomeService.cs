using System;
using System.Threading;
using System.Threading.Tasks;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;

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
        public async Task SendMessageAsync(string stockCode, CancellationToken cancellationToken, bool isDecoupledCall = false)
        {
            await _producerHandler.ProduceMessageAsync(stockCode, cancellationToken, isDecoupledCall);
            await _consumerHandler.ConsumeMessageAsync(cancellationToken, true);
        }
    }
}
