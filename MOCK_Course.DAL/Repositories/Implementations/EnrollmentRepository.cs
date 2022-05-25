using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(Enrollment _object)
        {
            _object.IsDeleted = true;
        }
    }
}
