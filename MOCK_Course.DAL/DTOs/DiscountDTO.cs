using System;

namespace Course.BLL.Responses
{
    public class DiscountDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        //public CourseDTO course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsActive { get; set; }
    }

    public class DiscountDTO_
    {
        public Guid Id { get; set; }
        //public Guid CourseId { get; set; }
        public CourseOfDiscountDTO course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsActive { get; set; }
    }
    public class CourseOfDiscountDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public class DiscountForUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    }
    public class DiscountForCreateDTO
    {
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }

    }
}
