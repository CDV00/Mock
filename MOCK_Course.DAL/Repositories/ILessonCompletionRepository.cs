using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ILectureCompletionRepository : IRepository<LectureCompletion, Guid>
    {
        Task<bool> IsCompletion(LectureCompletion lessonCompletion);
    }
}
