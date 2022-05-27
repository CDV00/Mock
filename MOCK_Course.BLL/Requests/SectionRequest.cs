using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        public Guid CourseId { get; set; }
        [Required]
        public string Title { get; set; }
    }
    public class SectionUpdateRequest
    {
        public string Title { get; set; }
    }
}
