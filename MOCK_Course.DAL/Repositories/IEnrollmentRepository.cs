using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
    }
}
