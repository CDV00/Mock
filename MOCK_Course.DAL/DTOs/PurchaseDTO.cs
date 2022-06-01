﻿using System;

namespace Course.DAL.DTOs
{
    public class PurchaseDTO
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
