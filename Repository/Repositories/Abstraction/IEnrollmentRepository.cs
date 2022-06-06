using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
    {
        //Task<bool> IsEnrollmented(Enrollment enrollment);
        //Task<int> GetTotal(Guid userId);
        IEnrollmentQuery BuildQuery();
    }
}
