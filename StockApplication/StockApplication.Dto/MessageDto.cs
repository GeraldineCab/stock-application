using System;

namespace StockApplication.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
    }
}
