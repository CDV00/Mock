using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class DiscountDTO
    {
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }

    }
 }
