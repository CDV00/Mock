using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ILectureCompletionRepository : IRepository<LectureCompletion>
    {
        ILectureCompletionQuery BuildQuery();
        Task<bool> IsCompletion(LectureCompletion lessonCompletion);
    }
}
