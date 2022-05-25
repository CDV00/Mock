using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ILessonRepository : IRepository<Lesson, Guid>
    {
    }
}
