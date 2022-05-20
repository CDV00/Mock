using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Section : BaseEntity<Guid>
    {
        public string Title { get; set; }
        //public int? TotalTime { get; set; }

        public Guid CourseId { get; set; }
        public Courses Course { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
