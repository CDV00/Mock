using Course.BLL.Responses;
using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class DiscounRequest
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    }
    public class DiscountForUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    }
    public class DiscountForCreateRequest
    {
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
