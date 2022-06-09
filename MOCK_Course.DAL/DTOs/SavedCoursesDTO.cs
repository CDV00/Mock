using Course.BLL.Responses;
using System;

namespace Course.BLL.DTO
{
    public class SavedCoursesDTO
    {
        public Guid Id { get; set; }
        //public UserDTO User { get; set; }
        public CourseDTO Course { get; set; }

    }
    //public class CartUserDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Fullname { get; set; }
    //}
    //public class CartCourseDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Title { get; set; }
    //    public decimal Price { get; set; }
    //    public CartCategoryDTO Category { get; set; }
    //}
    //public class CartCategoryDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //}
}
