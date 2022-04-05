using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockApplication.Persistence.Repositories.Interfaces;

namespace StockApplication.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IStockApplicationContext _context;
        internal DbSet<T> DbSet;

        public BaseRepository(IStockApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = (_context as DbContext)?.Set<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
        
        /// <inheritdoc />
        public void Add(T entity)
        {
            DbSet?.Add(entity);
        }
        
        /// <inheritdoc />
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return (_context as DbContext)?.SaveChangesAsync(cancellationToken);
        }
    }
}
