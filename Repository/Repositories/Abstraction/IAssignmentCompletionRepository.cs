using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Query.Abstraction;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstraction
{
    public interface IAssignmentCompletionRepository : IRepository<AssignmentCompletion>
    {
        IAssignmentCompletionQuery BuildQuery();
        Task<bool> IsCompletion(AssignmentCompletion assignmentCompletion);
    }
}