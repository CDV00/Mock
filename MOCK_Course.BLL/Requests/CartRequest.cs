using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CartRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        public bool IsActive { get; set; }
    }
    public class CartUpdateRequest
    {
        public bool IsActive { get; set; }
    }
}
