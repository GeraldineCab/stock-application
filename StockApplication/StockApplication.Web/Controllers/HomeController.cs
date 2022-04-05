using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProducerHandler _producerHandler;
        private readonly IConsumerHandler _consumerHandler;
        private readonly IMessageService _messageService;

        public HomeController(IProducerHandler producerHandler, IConsumerHandler consumerHandler, IMessageService messageService)
        {
            _producerHandler = producerHandler ?? throw new ArgumentNullException(nameof(producerHandler));
            _consumerHandler = consumerHandler ?? throw new ArgumentNullException(nameof(consumerHandler));
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
            await _producerHandler.ProduceMessageAsync(stockCode, cancellationToken);
            var stockModel = await _consumerHandler.ConsumeMessageAsync(cancellationToken);
            return View("Index", stockModel);
        }
    }
}
