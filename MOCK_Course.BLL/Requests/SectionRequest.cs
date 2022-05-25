using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        [Required]
        public string Title { get; set; }
    }
    public class SectionUpdateRequest 
    {
        [Required]
        public string Title { get; set; }

    }
}
