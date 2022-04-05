using StockApplication.Persistence.Entities;
using StockApplication.Persistence.Repositories.Interfaces;

namespace StockApplication.Persistence.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IStockApplicationContext context) : base(context)
        {
            
        }
    }
}
