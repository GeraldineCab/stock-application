using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Business.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Adds test users in the backing storage
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddUsersAsync(CancellationToken cancellationToken);
    }
}
