using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
        Task<bool> IsEnrollmented(Enrollment enrollment);
    }
}
