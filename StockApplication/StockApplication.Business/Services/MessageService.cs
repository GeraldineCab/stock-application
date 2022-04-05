using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockApplication.Business.Extensions;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Dto;
using StockApplication.Persistence.Entities;
using StockApplication.Persistence.Repositories.Interfaces;

namespace StockApplication.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }
        
        /// <inheritdoc />
        public async Task<List<MessageDto>> GetMessagesAsync(CancellationToken cancellationToken)
        {
            var mapper = new MessageExtensions().Mapper;
            var messages = await _messageRepository
                .GetAll()
                .Include(m => m.User)
                .OrderBy(m => m.Date)
                .Take(50).ToListAsync(cancellationToken);
            if (messages.Any())
            {
                return messages.Select(m => mapper.Map<MessageDto>(m)).ToList();
            }
            return null;
        }


        /// <inheritdoc />
        public Task AddMessageAsync(MessageDto message, CancellationToken cancellationToken)
        {
            var mapper = new MessageExtensions().Mapper;
            var messageEntity = mapper.Map<Message>(message);
           _messageRepository.Add(messageEntity);
            return _messageRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
