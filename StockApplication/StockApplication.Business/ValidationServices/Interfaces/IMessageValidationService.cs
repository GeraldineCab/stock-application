using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StockApplication.Business.ValidationServices.Interfaces
{
    public interface IMessageValidationService
    {
        /// <summary>
        /// Checks is the given stock code is valid
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        (ValidationResult,string) ValidateStockCode(string stockCode);
    }
}
