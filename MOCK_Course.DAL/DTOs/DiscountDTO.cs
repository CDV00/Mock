﻿using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class DiscountDTO
    {
        public Guid Id { get; set; }
        public CourseDTO course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsActive { get; set; }
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