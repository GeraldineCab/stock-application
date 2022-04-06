using StockApplication.Persistence.Entities;
using StockApplication.Persistence.Repositories.Interfaces;

namespace StockApplication.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IStockApplicationContext context) : base(context)
        {
            
        }
    }
}
