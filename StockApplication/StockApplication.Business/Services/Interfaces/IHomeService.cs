using System.Threading;
using System.Threading.Tasks;
using StockApplication.Dto;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IHomeService
    {
        /// <summary>
        /// Encapsulates the send message logic
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <param name="isDecoupledCall"></param>
        /// <returns></returns>
        Task<MessageDto> SendMessageAsync(string stockCode, CancellationToken cancellationToken = default, bool isDecoupledCall = false);
    }
}
