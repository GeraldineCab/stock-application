using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Common.Constants;
using StockApplication.Common.Messages;
using StockApplication.Dto;

namespace StockApplication.Business.Services
{
    public class StockService : HttpClientHelper, IStockService
    {
        private readonly IMessageValidationService _messageValidationService;

        public StockService(HttpClient httpClient, IMessageValidationService messageValidationService) 
            : base(httpClient)
        {
            _messageValidationService = messageValidationService ?? throw new ArgumentNullException(nameof(messageValidationService));
        }

        /// <inheritdoc />
        public async Task<StockDto> GetStockAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false)
        {
            var(validationResult, stockCodeFormatted) = _messageValidationService.ValidateStockCode(stockCode);
            
            if (commandNeeded && !string.IsNullOrEmpty(validationResult.ErrorMessage))
            {
                return null;
            }

            StockDto stock;
            var uri = string.Format(ConnectionNames.StockApi, stockCodeFormatted);
            var response = await GetStreamAsync(uri, cancellationToken);

            if (response == null)
            {
                return null;
            }

            using var reader = new StreamReader(response);
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                stock = csv.GetRecord<StockDto>();
            };
            
            return stock;
        }

        /// <inheritdoc />
        public async Task<string> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false)
        {
            var stock = await GetStockAsync(stockCode, cancellationToken, commandNeeded);
            return string.Format(StockMessages.ClosePriceMessage, stock.Symbol.ToUpper(), stock.Close);
        }
    }
}
