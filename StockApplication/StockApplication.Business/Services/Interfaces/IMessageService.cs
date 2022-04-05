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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<MessageDto>> GetMessagesAsync(CancellationToken cancellationToken);
    }
}
