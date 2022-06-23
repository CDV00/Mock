using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICourseReviewRepository : IRepository<CourseReview>
    {
        ICourseReviewQuery BuildQuery();
        Task<PagedList<CourseReviewDTO>> GetAllCourseReview(Guid courseId, CourseReviewParameters parameter);
        Task<float> GetMyRating(Guid userId, Guid courseId);
    }
}
