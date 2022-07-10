using Course.DAL.Data;
using Entities.Models;
using Repository.Repositories;

namespace MOCK_Course.DAL.Repositories.Implementations
{
    public class MessageChatRepository : Repository<MessageChat>, IMessageChatRepository
    {
        public MessageChatRepository(AppDbContext context) : base(context)
        {
        }
    }
}
