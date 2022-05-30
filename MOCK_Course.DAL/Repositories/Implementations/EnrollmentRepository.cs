using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class EnrollmentRepository : Repository<Enrollment, Guid>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context): base(context)
        {

        }

        public async Task<bool> IsEnrollmented(Enrollment enrollment)
        {
            if (await FindByCondition(l => l.UserId == enrollment.UserId && l.CourseId == enrollment.CourseId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
