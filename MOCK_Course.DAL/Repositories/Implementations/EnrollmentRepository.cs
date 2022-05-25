using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class EnrollmentRepository : Repository<Enrollment, Guid>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context): base(context)
        {

        }
    }
}
