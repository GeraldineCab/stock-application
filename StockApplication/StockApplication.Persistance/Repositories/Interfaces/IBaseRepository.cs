using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockApplication.Persistence.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Gets all records of the given type
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Adds a record of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Add(T entity);

        /// <summary>
        /// Persists data
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
