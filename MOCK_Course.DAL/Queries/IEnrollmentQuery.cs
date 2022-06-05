using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface IEnrollmentQuery : IQuery<Enrollment>
    {
        IEnrollmentQuery FilterByCourseId(Guid CourseId);
        IEnrollmentQuery FilterByUserId(Guid UserId);
        IEnrollmentQuery IncludeCourse();
        IEnrollmentQuery IncludeUser();
    }
}