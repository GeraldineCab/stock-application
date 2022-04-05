using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockApplication.Business.Extensions;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Dto;
using StockApplication.Persistence;

namespace StockApplication.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IStockApplicationContext _context;

        public MessageService(IStockApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<MessageDto>> GetMessagesAsync(CancellationToken cancellationToken)
        {
            var mapper = new MessageExtensions().Mapper;
            var messages = await _context.Messages.ToListAsync(cancellationToken);
            if (messages.Any())
            {
                return messages.Select(m => mapper.Map<MessageDto>(m)).ToList();
            }
            return null;
        }
    }
}
