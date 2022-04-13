using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace StockApplication.Persistence.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public bool IsBot { get; set; } = false;
        public string Password { get; set; }
    }
}
