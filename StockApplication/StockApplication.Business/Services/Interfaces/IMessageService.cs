using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StockApplication.Dto;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Gets messages
        /// </summary>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        Task<List<MessageDto>> GetMessagesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Adds a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        Task AddMessageAsync(MessageDto message, CancellationToken cancellationToken);
    }
}
