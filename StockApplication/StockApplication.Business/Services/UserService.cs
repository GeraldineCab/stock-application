using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StockApplication.Business.Services.Interfaces;

namespace StockApplication.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IHttpContextAccessor contextAccessor)
        {   
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }
        /// <inheritdoc />
        public string GetUsername()
        {
            return  _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
