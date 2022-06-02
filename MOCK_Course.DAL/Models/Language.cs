using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class AudioLanguage : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Courses> Courses { get; set; }
    }

    public class CloseCaption : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Courses> Courses { get; set; }
    }
}
