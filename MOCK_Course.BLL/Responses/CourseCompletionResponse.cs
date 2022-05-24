using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class CourseCompletionResponse
    {
        public Guid Id { get; set; }
        public CourseCompletionUser CourseCompUser { get; set; }
        public CourseCompletionCourse Course { get; set; }
    }
    public class CourseCompletionUser
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class CourseCompletionCourse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CourseCompletionCategory category {get;set;}
    }
    public  class CourseCompletionCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
