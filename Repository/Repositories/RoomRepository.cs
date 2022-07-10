using Course.DAL.Data;
using Entities.Models;
using Repository.Repositories;

namespace MOCK_Course.DAL.Repositories.Implementations
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }
    }
}
