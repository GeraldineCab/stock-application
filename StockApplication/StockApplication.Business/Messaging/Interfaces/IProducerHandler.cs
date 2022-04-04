using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Messaging.Interfaces
{
    public interface IProducerHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        Task ProduceMessageAsync(string stockCode, CancellationToken cancellationToken);
    }
}
