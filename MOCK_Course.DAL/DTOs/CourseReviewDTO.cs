﻿using System;

namespace Course.BLL.Responses
{
    public class CourseReviewDTO
    {
        public Guid Id;
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
