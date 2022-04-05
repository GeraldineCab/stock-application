using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Common.Constants;
using StockApplication.Common.Messages;
using StockApplication.Dto;
using StockApplication.Persistence;

namespace StockApplication.Business.Services
{
    public class StockService : HttpClientHelper, IStockService
    {
        private readonly IStockApplicationContext _context;
        private readonly IMapper _mapper;

        public StockService(HttpClient httpClient, IStockApplicationContext context, IMapper mapper) : base(httpClient)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<StockDto> GetStockAsync(string stockCode, CancellationToken cancellationToken, bool commandNeeded = false)
        {
            if (commandNeeded)
            {
                if (!stockCode.StartsWith(Commands.GetStock) || string.IsNullOrEmpty(stockCode))
                {
                    return null;
                }
            }

            StockDto stock;
            var uri = string.Format(ConnectionNames.StockApi, stockCode.Split('=')[1]);
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
