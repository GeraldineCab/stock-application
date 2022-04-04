using System.Threading;
using System.Threading.Tasks;
using StockApplication.Dto;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IStockService
    {
        /// <summary>
        /// Gets stock information
        /// </summary>
        /// <param name="stockCode">The stock code</param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns>A new instance of <see cref="Stock"/></returns>
        Task<Stock> GetStockAsync(string stockCode, CancellationToken cancellationToken);

        /// <summary>
        /// Gets stock close price information
        /// </summary>
        /// <param name="stockCode">The stock code</param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns>A message with the information</returns>
        Task<string> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken);
    }
}
