using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using StockApplication.Business.Services;
using Telerik.JustMock;

namespace StockApplication.Business.Tests.Services
{
    [TestFixture]
    public class StockServiceTests
    {
        private string _validStockCode = "AAPL.US";
        private string _invalidStockCode = "MyStockCode";
        private StockService _stockService;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _stockService = new StockService(_httpClient);
        }

        [Test]
        public async Task Do_GetStockAsync_When_StockIsFound_Returns_StockDto()
        {
            // Act
            var result = await _stockService.GetStockAsync(_validStockCode, Arg.IsAny<CancellationToken>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Symbol, _validStockCode);
        }

        [Test]
        public async Task Do_GetStockAsync_When_StockIsNotFound_Returns_Null()
        {
            // Act
            var result = await _stockService.GetStockAsync(_invalidStockCode, Arg.IsAny<CancellationToken>());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Do_GetStockClosePriceAsync_When_StockIsFound_Returns_String()
        {
            // Act
            var result = await _stockService.GetStockClosePriceAsync(_validStockCode, Arg.IsAny<CancellationToken>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("quote"));
        }

        [Test]
        public async Task Do_GetStockClosePriceAsync_When_StockIsNotFound_Returns_Null()
        {
            // Act
            var result = await _stockService.GetStockClosePriceAsync(_invalidStockCode, Arg.IsAny<CancellationToken>());

            // Assert
            Assert.IsNull(result);
        }
    }
}
