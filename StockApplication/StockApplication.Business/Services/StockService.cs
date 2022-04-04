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
        public StockService(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc />
        public async Task<Stock> GetStockAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false)
        {
            if (commandNeeded)
            {
                if (!stockCode.StartsWith(Commands.GetStock) || string.IsNullOrEmpty(stockCode))
                {
                    return null;
                }
            }

            Stock stock;
            var uri = string.Format(ConnectionNames.StockApi, stockCode);
            var response = await GetStreamAsync(uri, cancellationToken);

            if (response == null)
            {
                return null;
            }

            using var reader = new StreamReader(response);
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                stock = csv.GetRecord<Stock>();
            };

            return stock;
        }

        /// <inheritdoc />
        public async Task<string> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false)
        {
            if (commandNeeded)
            {
                if (!stockCode.StartsWith(Commands.GetStock) || string.IsNullOrEmpty(stockCode))
                {
                    return null;
                }
            }

            var stock = await GetStockAsync(stockCode, cancellationToken);
            return string.Format(StockMessages.ClosePriceMessage, stock.Symbol.ToUpper(), stock.Close);
        }
    }
}
