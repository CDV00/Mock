using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICourseReviewRepository : IRepository<CourseReview, Guid>
    {
        Task<int> GetTotal(Guid userId);
    }
}
