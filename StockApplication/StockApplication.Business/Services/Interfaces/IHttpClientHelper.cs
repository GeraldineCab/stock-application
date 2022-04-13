using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Gets resource as a stream from the given uri 
        /// </summary>
        /// <param name="uri">The resource uri</param>
        /// <param name="cancellationToken">Transaction cancellation token</param>
        /// <returns></returns>
        Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken);

        /// <summary>
        /// Gets resource as string from the given uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string uri, CancellationToken cancellationToken);
    }
}
