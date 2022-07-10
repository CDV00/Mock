using Course.DAL.Data;
using Entities.Models;
using Repository.Repositories;

namespace MOCK_Course.DAL.Repositories.Implementations
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext context) : base(context)
        {
        }
    }
}
