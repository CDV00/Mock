using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Responses
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        public SubscriptionUserDTO SubscriptionUser { get; set; }
    }
    public class SubscriptionUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
}
