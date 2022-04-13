using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Business.Services
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <inheritdoc />
        public Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken)
        {
            return _httpClient.GetStreamAsync(uri, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetStringAsync(string uri, CancellationToken cancellationToken)
        {
            return _httpClient.GetStringAsync(uri, cancellationToken);
        }
    }
}
