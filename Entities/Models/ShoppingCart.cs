﻿using System;

namespace Course.DAL.Models
{
    public class ShoppingCart
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }

        public AppUser User { get; set; }
        public Courses Course { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
