using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockApplication.Persistence.Entities;

namespace StockApplication.Persistence
{
    public class StockApplicationContext : DbContext, IStockApplicationContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=StockContext")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging(); // This must be used only for debugging purposes since this method shows the parameters' values
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .Property(m => m.Date)
                .HasDefaultValueSql("getdate()");
        }
    }
}
