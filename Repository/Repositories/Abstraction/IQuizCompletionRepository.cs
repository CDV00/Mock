using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Query.Abstraction;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstraction
{
    public interface IQuizCompletionRepository : IRepository<QuizCompletion>
    {
        IQuizCompletionQuery BuildQuery();
        Task<bool> IsCompletion(QuizCompletion lessonCompletion);
    }
}