using Course.DAL.Data;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseReviewRepository: Repository<CourseReview>, ICourseReviewRepository
    {
        public CourseReviewRepository(AppDbContext context): base(context) { }

        public override void Remove(CourseReview _object)
        {
            base.Remove(_object);
        }
    }
}
