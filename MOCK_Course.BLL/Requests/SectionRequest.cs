using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionRequest
    {
        [Required]
        public string Title { get; set; }
        public ICollection<LessonRequest> LessonRequests { get; set; }
    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<LessonRequest> LessonRequests { get; set; }
    }
}
