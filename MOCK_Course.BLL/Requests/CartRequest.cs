using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CartRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
