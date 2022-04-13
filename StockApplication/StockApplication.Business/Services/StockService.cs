using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Constants;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Services
{
    public class StockService : HttpClientHelper, IStockService
    {
        public StockService(HttpClient httpClient) 
            : base(httpClient) { }

        /// <inheritdoc />
        public async Task<StockDto> GetStockAsync(string stockCode, CancellationToken cancellationToken)
        {
            StockDto stockDto;
            var uri = string.Format(ConnectionNames.StockApi, stockCode);
            var response = await GetStreamAsync(uri, cancellationToken);
            var responseIsInvalid = (await GetStringAsync(uri, cancellationToken)).Contains("N/D");

            if (responseIsInvalid)
            {
                return null;
            }
            
            using var reader = new StreamReader(response);
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                stockDto = csv.GetRecord<StockDto>();
            };
            
            return stockDto;
        }

        /// <inheritdoc />
        public async Task<string> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken)
        {
            var stock = await GetStockAsync(stockCode, cancellationToken);
            return stock != null ? string.Format(StockMessages.ClosePriceMessage, stock.Symbol.ToUpper(), stock.Close) : null;
        }
    }
}
