using Course.BLL.Requests;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        IAssignmentQuery BuildQuery();
        Task<PagedList<AssignmentDTO>> GetAllAssignment(AssignmentParameters parameter);
    }
}
