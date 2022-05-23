using Course.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionRequest
    {
        [Required]
        public string Title { get; set; }
        public int? TotalTime { get; set; }
        public Guid CourseId { get; set; }

    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
