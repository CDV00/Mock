using System;

namespace Course.BLL.DTO
{
    public class CoursesCardDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public CategoryDTO CategoryResponse { get; set; }
        public UserDTO UserResponse { get; set; }
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
    }
}
