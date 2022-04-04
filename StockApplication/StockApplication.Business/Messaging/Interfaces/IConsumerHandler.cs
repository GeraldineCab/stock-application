using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Messaging.Interfaces
{
    public interface IConsumerHandler
    {
        /// <summary>
        /// Consumes and handle message received
        /// </summary>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        Task<string> ConsumeMessageAsync(CancellationToken cancellationToken);
    }
}
