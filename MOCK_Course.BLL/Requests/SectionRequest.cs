using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionRequest
    {
        [Required]
        public string Title { get; set; }
    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
