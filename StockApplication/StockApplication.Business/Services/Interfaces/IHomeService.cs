using System.Threading;
using System.Threading.Tasks;

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
        Task SendMessageAsync(string stockCode, CancellationToken cancellationToken, bool isDecoupledCall = false);
    }
}
