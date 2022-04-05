using System.Collections.Generic;

namespace StockApplication.Dto
{
    public class UserDto
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
