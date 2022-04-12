using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Messaging.Interfaces
{
    public interface IProducerHandler
    {
        /// <summary>
        /// Produces a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <param name="isDecoupledCall"></param>
        /// <returns></returns>
        Task ProduceMessageAsync(string message, CancellationToken cancellationToken, bool isDecoupledCall = false);
    }
}
