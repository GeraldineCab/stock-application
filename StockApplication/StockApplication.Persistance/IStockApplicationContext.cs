using Microsoft.EntityFrameworkCore;
using StockApplication.Persistence.Entities;

namespace StockApplication.Persistence
{
    public interface IStockApplicationContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}
