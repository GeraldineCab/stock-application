using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Messaging.Interfaces
{
    public interface IProducerHandler
    {
        /// <summary>
        /// Produces a message
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <param name="commandNeeded"></param>
        /// <param name="isDecoupledCall"></param>
        /// <returns></returns>
        Task<bool> ProduceMessageAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false, bool isDecoupledCall = false);
    }
}
