using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IMessageService _messageService;

        public HomeController(IHomeService homeService, IMessageService messageService)
        {
            _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var messages = await _messageService.GetMessagesAsync(cancellationToken);
            return View(messages);
        }

        /// <summary>
        /// Sends message and consumes it
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(string stockCode, CancellationToken cancellationToken = default)
        {
            await _homeService.SendMessageAsync(stockCode, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
