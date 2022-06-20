using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Responses
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubscriberId { get; set; }
    }
    public class SubscriptionUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
}
