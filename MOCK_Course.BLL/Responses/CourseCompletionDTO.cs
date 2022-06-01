using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class CourseCompletionDTO
    {
        public Guid Id { get; set; }
        public CourseCompletionUserDTO User { get; set; }
        public CourseCompletionCourseDTO Course { get; set; }
    }
    public class CourseCompletionUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class CourseCompletionCourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CourseCompletionCategoryDTO Category {get;set;}
    }
    public  class CourseCompletionCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
