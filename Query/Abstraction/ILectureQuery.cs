using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface ILectureQuery : IQuery<Lecture>
    {
        ILectureQuery FilterBySectionId(Guid sectionId);
        ILectureQuery FilterLecturebyCourse(Guid courseId);
    }
}