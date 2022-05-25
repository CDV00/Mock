using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Responses
{
    public class CourseReviewResponse
    {
        public Guid Id;
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
