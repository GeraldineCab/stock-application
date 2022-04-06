using System.Collections.Generic;

namespace StockApplication.Dto
{
    public class UserDto
    {        
        public string Name { get; set; }
        public bool IsBot { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
