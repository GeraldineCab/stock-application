﻿using Microsoft.AspNetCore.Identity;
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

        public DbSet<Message> Messages { get; set; }

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
                        UserName = "josh@mail.com",
                        Email = "josh@mail.com",
                        NormalizedEmail = "josh@mail.com",
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                        Password = "J123osh",
                        IsBot = false
                    },
                    new User()
                    {
                        UserName = "mary@mail.com",
                        Email = "mary@mail.com",
                        NormalizedEmail = "mary@mail.com",
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                        Password = "M123ary",
                        IsBot = false
                    },
                    new User()
                    {
                        UserName = "Bot",
                        Email = "bot@mail.com",
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                        Password = "PasswordBot"
                    });

           modelBuilder.Entity<Message>()
               .Property(m => m.Date)
               .HasDefaultValueSql("getdate()");
        }
    }
}
