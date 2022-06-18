using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class DiscountForUpdateRequest : DiscountForCreateRequest
    {
        //public Guid CourseId { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public decimal DiscountPercent { get; set; }
    }
    public class DiscountForCreateRequest
    {
        //[Required(ErrorMessage = "Course Id is a Required filed.")]
        //public Guid CourseId { get; set; }

        [Required(ErrorMessage = "Start Date is a Required filed.")]
        // Add validation Start Date must > Now
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate Date is a Required filed.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Discount is a Required filed.")]
        [Range(0.0, 100.0, ErrorMessage = "Discount must be from 0 to 100")]
        public decimal DiscountPercent { get; set; }
    }
}
