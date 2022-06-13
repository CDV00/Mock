using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    public interface ICourseReviewQuery : IQuery<CourseReview>
    {
        ICourseReviewQuery FilterByCourseId(Guid? CourseId);
        ICourseReviewQuery FilterByRating(float Rating);
        ICourseReviewQuery FilterByUserId(Guid UserId);
        ICourseReviewQuery FilterByKeyword(string Keyword);
        Task<float> GetAvgRate();
        Task<float> GetAvgRatePercent(long sum);
        ICourseReviewQuery IncludeCourse();
        ICourseReviewQuery IncludeEnrollment();
        ICourseReviewQuery IncludeUser();
        ICourseReviewQuery FilterByUserId(Guid? userId);
        ICourseReviewQuery GetById(Guid id);
        ICourseReviewQuery FilterByUserIdOfCourse(Guid UserId);
        ICourseReviewQuery FilterCourseByUSer(Guid userId);
    }
}