using System.ComponentModel.DataAnnotations;
using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Common.Messages;

namespace StockApplication.Business.ValidationServices
{
    public class MessageValidationService : IMessageValidationService
    {
        /// <inheritdoc />
        public (ValidationResult, string) ValidateStockCode(string stockCode)
        {
            var validationResult = new ValidationResult("");
            if (!stockCode.StartsWith(Commands.GetStock) || string.IsNullOrEmpty(stockCode))
            {
                validationResult.ErrorMessage = "Stock command was not sent or it is not valid";
                return (validationResult, null);
            }

            var stockCodeFormatted = stockCode.Split('=')[1];
            return (validationResult, stockCodeFormatted);
        }
    }
}
