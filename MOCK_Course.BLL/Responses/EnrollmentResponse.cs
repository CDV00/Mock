using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class EnrollmentResponse
    {
        public Guid Id { get; set; }
        public EnrollmentUser EnrollmentUser { get; set; }
        public EnrollmentCourse Course { get; set; }
    }
    public class EnrollmentUser
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class EnrollmentCourse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public EnrollmentCategory category {get;set;}
    }
    public  class EnrollmentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
