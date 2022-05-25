using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ILessonCompletionRepository : IRepository<LessonCompletion, Guid>
    {
    }
}
