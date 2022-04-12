using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Business.Messaging.Interfaces;

namespace StockApplication.Bot.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockBotController : ControllerBase
    {
        private readonly IConsumerHandler _consumerHandler;
        private readonly IProducerHandler _producerHandler;

        public StockBotController(IConsumerHandler consumerHandler, IProducerHandler producerHandler)
        {
            _consumerHandler = consumerHandler ?? throw new ArgumentNullException(nameof(consumerHandler));
            _producerHandler = producerHandler ?? throw new ArgumentNullException(nameof(producerHandler));
        }

        /// <summary>
        /// Gets a stock close price by stock code
        /// </summary>
        /// <param name="stockCode">The stock code</param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns>A message with the stock close price</returns>
        [HttpGet("{stockCode}")]
        public async Task<ActionResult> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken = default)
        {
            await _producerHandler.ProduceMessageAsync(stockCode, cancellationToken, true);
            var response = await _consumerHandler.ConsumeMessageAsync(cancellationToken, true);

            if (string.IsNullOrEmpty(response))
            {
                return new NotFoundObjectResult("The bot is unable to provide stock information for the given message.");
            }

            return new OkObjectResult(response);
        }
    }
}
