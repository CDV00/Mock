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
        public CourseCategoryDTO Category { get; set; }
        public UserDTO User { get; set; }
    }

    public class CourseCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
