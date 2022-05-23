using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class CousesRepository : Repository<Courses>, ICousesRepository
    {
        public CousesRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(Courses _object)
        {
            _object.IsDeleted = true;
        }
    }
}
