using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{ 
    public interface ILectureCompletionQuery : IQuery<LectureCompletion>
    {
        ILectureCompletionQuery FilterLectureCompletion(Guid userId, Guid sectionId);
        ILectureCompletionQuery FilterLectureCompletionCourse(Guid userId, Guid courseId);
    }
}
