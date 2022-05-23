using System;

namespace Course.BLL.Responses
{
    public class CoursesResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public CategoryCourseRespones CategoryResponse { get; set; } 
        public UserCourseResponse UserResponse { get; set; }
    }

    public class CategoryCourseRespones
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UserCourseResponse
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    } 
}
