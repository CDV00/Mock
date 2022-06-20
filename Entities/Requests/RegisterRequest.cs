using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(20)]
        public string Fullname { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        /// <summary>
        /// category of course
        /// </summary>
        public Guid? CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
