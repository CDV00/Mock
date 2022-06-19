using Microsoft.AspNetCore.Identity;
using System;

namespace Course.DAL.Models
{
    public class UserBase : IdentityUser<Guid>
    {
        public UserBase()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
