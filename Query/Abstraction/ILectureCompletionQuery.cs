using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{ 
    public interface ILectureCompletionQuery : IQuery<LectureCompletion>
    {
        ILectureCompletionQuery FilterByLecture(Guid lectureId);
        ILectureCompletionQuery FilterByUser(Guid userId);
        ILectureCompletionQuery FilterLectureCompletion(Guid userId, Guid sectionId);
        ILectureCompletionQuery FilterLectureCompletionCourse(Guid userId, Guid courseId);
    }
}
