using Microsoft.EntityFrameworkCore;
using StockApplication.Persistence.Entities;

namespace StockApplication.Persistence
{
    public interface IStockApplicationContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
