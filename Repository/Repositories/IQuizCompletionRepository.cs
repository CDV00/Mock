using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Query;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IQuizCompletionRepository : IRepository<QuizCompletion>
    {
        IQuizCompletionQuery BuildQuery();
        Task<bool> IsCompletion(QuizCompletion lessonCompletion);
    }
}