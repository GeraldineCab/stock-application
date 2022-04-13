using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Web.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IHomeService _homeService;
        public MessageHub(IHomeService homeService)
        {
            _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
        }
        public async Task SendMessageAsync(string message, CancellationToken cancellationToken)
        {
            var messageDto = await _homeService.SendMessageAsync(message, cancellationToken);
            await Clients.All.SendAsync("ReceiveMessage", messageDto.Username, messageDto.Text, messageDto.Date, cancellationToken);
        }
    }
}
