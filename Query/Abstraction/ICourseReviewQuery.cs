using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface ICourseReviewQuery : IQuery<CourseReview>
    {
        ICourseReviewQuery FilterByCourseId(Guid CourseId);
        ICourseReviewQuery FilterByUserId(Guid UserId);
        ICourseReviewQuery IncludeCourse();
        ICourseReviewQuery IncludeEnrollment();
        ICourseReviewQuery IncludeUser();
    }
}