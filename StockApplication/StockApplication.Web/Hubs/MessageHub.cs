using System;
using System.Linq;
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

        public async Task SendMessageAsync(string message)
        {
            var messages = await _homeService.SendMessageAsync(message, isDecoupledCall: false);

            messages.Select(async m =>
                await Clients.All.SendAsync("ReceiveMessage", m.Username, m.Text, m.Date.ToString("g")));
        }
    }
}
