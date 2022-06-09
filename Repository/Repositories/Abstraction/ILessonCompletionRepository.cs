using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ILectureCompletionRepository : IRepository<LectureCompletion>
    {
        Task<bool> IsCompletion(LectureCompletion lessonCompletion);
    }
}
