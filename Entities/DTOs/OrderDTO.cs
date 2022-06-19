﻿using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class OrderDTO
    {
        public Guid UserId { get; set; }
        public List<OrderItemDTO> OrderItem { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid CourseId { set; get; }
    }
}