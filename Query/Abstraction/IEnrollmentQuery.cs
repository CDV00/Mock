using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IEnrollmentQuery : IQuery<Enrollment>
    {
        IEnrollmentQuery FilterByCourseId(Guid? CourseId);
        IEnrollmentQuery FilterByCourseUserId(Guid? UserId);
        IEnrollmentQuery FilterByUserId(Guid? UserId);
        IEnrollmentQuery IncludeCourse();
        IEnrollmentQuery IncludeUser();
    }
}