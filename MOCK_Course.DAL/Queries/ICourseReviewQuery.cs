using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
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