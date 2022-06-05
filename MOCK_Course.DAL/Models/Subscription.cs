﻿using System;

namespace Course.DAL.Models
{
    public class Subscription : BaseEntity<Guid>
    {
        public Guid SubscriberId { get; set; }
        public AppUser Subscriber { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}
