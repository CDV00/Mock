using Course.DAL.Models;
using Course.DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICourseReviewRepository : IRepository<CourseReview>
    {
    }
}
