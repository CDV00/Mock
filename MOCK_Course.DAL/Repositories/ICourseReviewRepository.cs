using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ICourseReviewRepository : IRepository<CourseReview, Guid>
    {
    }
}
