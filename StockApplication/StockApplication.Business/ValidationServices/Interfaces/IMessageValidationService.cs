using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StockApplication.Business.ValidationServices.Interfaces
{
    public interface IMessageValidationService
    {
        /// <summary>
        /// Gets the stock command
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string GetStockCommand(string message);
    }
}
