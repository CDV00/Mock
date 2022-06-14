using System;

namespace Course.BLL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public int TotalSubcripbers { get; set; }
        public int TotalCourses { get; set; }
    }
}
