﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CourseCompletionRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public class CourseCompletionUpdateRequest : CourseCompletionRequest
    {
        //public Guid Id { get; set; }
    }
}