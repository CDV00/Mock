using System;

namespace Course.BLL.DTO
{
    public class CoursesCardDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; } = 0;
        public string ThumbnailUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryDTO Category { get; set; }
        public UserDTO User { get; set; }
    }

    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}
