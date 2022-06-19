using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CourseCompletionRequest
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
