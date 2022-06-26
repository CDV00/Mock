using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Query;
using Query.Abstraction;
using Repository.Repositories.Abstraction;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class QuizCompletionRepository : Repository<QuizCompletion>, IQuizCompletionRepository
    {
        private AppDbContext _context;
        public QuizCompletionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IQuizCompletionQuery BuildQuery() => new QuizCompletionQuery(_context.QuizCompletions.AsQueryable(), _context);

        public async Task<bool> IsCompletion(QuizCompletion lessonCompletion)
        {
            if (await FindByCondition(l => l.UserId == lessonCompletion.UserId && l.QuizId == lessonCompletion.QuizId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
