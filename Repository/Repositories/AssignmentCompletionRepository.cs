using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Query;
using Query.Abstraction;
using Repository.Repositories.Abstraction;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AssignmentCompletionRepository : Repository<AssignmentCompletion>, IAssignmentCompletionRepository
    {
        private AppDbContext _context;
        public AssignmentCompletionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IAssignmentCompletionQuery BuildQuery() => new AssignmentCompletionQuery(_context.AssignmentCompletions.AsQueryable(), _context);

        public async Task<bool> IsCompletion(AssignmentCompletion assignmentCompletion)
        {
            if (await FindByCondition(l => l.UserId == assignmentCompletion.UserId && l.AssignmentId == assignmentCompletion.AssignmentId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
