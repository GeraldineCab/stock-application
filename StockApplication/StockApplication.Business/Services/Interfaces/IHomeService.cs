using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IHomeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="commandNeeded"></param>
        /// <param name="isDecoupledCall"></param>
        /// <returns></returns>
        Task<bool> SendMessageAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false, bool isDecoupledCall = false);
    }
}
