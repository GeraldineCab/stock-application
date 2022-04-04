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

        public StockBotController(IConsumerHandler consumerHandler)
        {
            _consumerHandler = consumerHandler ?? throw new ArgumentNullException(nameof(consumerHandler));
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
            var response = await _consumerHandler.ConsumeMessageAsync(cancellationToken);

            if (string.IsNullOrEmpty(response))
            {
                return new NotFoundObjectResult("The bot is unable to provide stock information.");
            }

            return new OkObjectResult(response);
        }
    }
}
