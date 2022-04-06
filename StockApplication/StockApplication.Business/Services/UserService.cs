using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Persistence.Entities;

namespace StockApplication.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc />
        public async Task AddUsersAsync(CancellationToken cancellationToken)
        {
            var users = new List<User>()
            {
                new User()
                {
                    UserName = "josh@mail.com",
                    Email = "josh@mail.com",
                    Password = "J123osh"
                },
                new User()
                {
                    UserName = "mary@mail.com",
                    Email = "mary@mail.com",
                    Password = "M123ary"
                },
                new User()
                {
                    UserName = "Bot",
                    Email = "bot@mail.com",
                    Password = "PasswordBot"
                }
            };

            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    continue;
                }
            }

            var t = _userManager.Users;
        }
    }
}
