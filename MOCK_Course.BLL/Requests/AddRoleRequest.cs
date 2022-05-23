using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class AddRoleRequest
    {
        public Guid UserId { get; set; }
        /// <summary>
        /// category of course
        /// </summary>
        public Guid? CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
