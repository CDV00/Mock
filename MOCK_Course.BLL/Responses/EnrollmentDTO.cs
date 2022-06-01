using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class EnrollmentDTO
    {
        public Guid Id { get; set; }
        public EnrollmentUserDTO User { get; set; }
        public EnrollmentCourseDTO Course { get; set; }
    }
    public class EnrollmentUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class EnrollmentCourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public EnrollmentCategoryDTO Category {get;set;}
    }
    public  class EnrollmentCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
