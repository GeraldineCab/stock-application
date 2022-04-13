using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockApplication.Persistence.Entities;

namespace StockApplication.Persistence
{
    public class StockApplicationContext : IdentityDbContext<User, IdentityRole, string>, IStockApplicationContext
    {
        private readonly IConfiguration _configuration;
        public StockApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        NormalizedUserName = "Josh",
                        Email = "josh@mail.com",
                        NormalizedEmail = "josh@mail.com",
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                        Password = "J123osh"
                    },
                    new User()
                    {
                        NormalizedUserName = "Mary",
                        Email = "mary@mail.com",
                        NormalizedEmail = "mary@mail.com",
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                        Password = "M123ary"
                    });
        }
    }
}
