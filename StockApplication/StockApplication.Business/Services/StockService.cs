﻿using System;
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
        public async Task<StockDto> GetStockAsync(string stockCode, CancellationToken cancellationToken)
        {
            StockDto stockDto;
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
                stockDto = csv.GetRecord<StockDto>();
            };
            
            return stockDto;
        }

        /// <inheritdoc />
        public async Task<string> GetStockClosePriceAsync(string stockCode, CancellationToken cancellationToken)
        {
            var stock = await GetStockAsync(stockCode, cancellationToken);
            return string.Format(StockMessages.ClosePriceMessage, stock.Symbol.ToUpper(), stock.Close);
        }
    }
}
