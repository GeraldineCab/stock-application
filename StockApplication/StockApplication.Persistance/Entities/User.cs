using Microsoft.AspNetCore.Identity;

namespace StockApplication.Persistence.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
