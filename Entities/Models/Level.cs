using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Level : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Courses> Courses { get; set; }
    }
}
