using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CartRequest
    {
        public Guid UserId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
    }
}
