using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SavedCoursesRequest
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
