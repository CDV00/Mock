using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ILessonCompletionRepository : IRepository<LessonCompletion, Guid>
    {
        Task<bool> IsCompletion(LessonCompletion lessonCompletion);
    }
}
