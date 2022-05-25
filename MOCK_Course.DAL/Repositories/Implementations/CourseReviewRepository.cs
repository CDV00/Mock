using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseReviewRepository: Repository<CourseReview, Guid>, ICourseReviewRepository
    {
        public CourseReviewRepository(AppDbContext context): base(context) { }

        public override void Remove(CourseReview _object)
        {
            base.Remove(_object);
        }
    }
}
