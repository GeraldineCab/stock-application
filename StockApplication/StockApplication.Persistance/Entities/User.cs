using System.Collections.Generic;

namespace StockApplication.Persistence.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Message> Messages { get; set; }
    }
}
