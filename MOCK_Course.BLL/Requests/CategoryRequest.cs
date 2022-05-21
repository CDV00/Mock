using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CategoryRequest
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
    public class CategoryUpdateRequest : CategoryRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
