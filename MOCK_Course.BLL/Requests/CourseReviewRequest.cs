using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class CourseReviewRequest
    {
        public string Content { get; set; }
        [Required]
        public int Rating { get; set; }
        //[Required]
        //public Guid EnrollmentId { get; set; }
    }
    public class CourseReviewUpdateRequest : CourseReviewRequest
    {
        //[Required]
        //public Guid Id { get; set; }
    }
}
