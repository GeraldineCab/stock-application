using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Common.Utils;

namespace StockApplication.Business.ValidationServices
{
    public class MessageValidationService : IMessageValidationService
    {
        /// <inheritdoc />
        public string GetStockCommand(string message)
        {
            var (command, stock) = MessageUtils.GetStock(message);
            if (!string.IsNullOrEmpty(command))
            {
                return stock;
            }
            return string.Empty;
        }
    }
}
