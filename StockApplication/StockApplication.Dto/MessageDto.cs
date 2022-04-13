using System;

namespace StockApplication.Dto
{
    public class MessageDto
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
