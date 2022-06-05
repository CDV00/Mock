using Course.DAL.Models;
using Course.DAL.Queries;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
        //Task<bool> IsEnrollmented(Enrollment enrollment);
        //Task<int> GetTotal(Guid userId);
        IEnrollmentQuery BuildQuery();
    }
}
