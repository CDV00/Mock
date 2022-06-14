using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        IAssignmentQuery BuildQuery();
    }
}
