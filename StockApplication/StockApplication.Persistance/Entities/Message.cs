using System;

namespace StockApplication.Persistence.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
