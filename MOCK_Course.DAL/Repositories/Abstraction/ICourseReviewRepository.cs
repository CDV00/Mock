using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICourseReviewRepository : IRepository<CourseReview, Guid>
    {
        ICourseReviewQuery BuildQuery();
    }
}
