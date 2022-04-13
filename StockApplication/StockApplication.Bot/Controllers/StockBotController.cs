using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Bot.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockBotController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public StockBotController(IHomeService homeService)
        {
            _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
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
            var response = await _homeService.SendMessageAsync(stockCode, cancellationToken, true);

            if (string.IsNullOrEmpty(response.FirstOrDefault()?.Text))
            {
                return new BadRequestObjectResult("The bot is unable to provide stock information for the given message.");
            }

            return new OkObjectResult(response.FirstOrDefault()?.Text);
        }
    }
}
