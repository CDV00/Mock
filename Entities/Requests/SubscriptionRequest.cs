using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SubscriptionRequest
    {
        [Required]
        public Guid userId { get; set; }
    }
}
