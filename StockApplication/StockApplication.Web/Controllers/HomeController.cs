using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Business.Messaging.Interfaces;

namespace StockApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProducerHandler _producerHandler;

        public HomeController(IProducerHandler producerHandler)
        {
            _producerHandler = producerHandler ?? throw new ArgumentNullException(nameof(producerHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string stockCode, CancellationToken cancellationToken = default)
        {
            await _producerHandler.ProduceMessageAsync(stockCode, cancellationToken);
            return View();
        }
    }
}
