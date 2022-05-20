using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class RegisterRequest 
    {
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
