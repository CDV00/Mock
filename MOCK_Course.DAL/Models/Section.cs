using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Section : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public int TotalTime { get; set; } = 0;

        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public List<Lecture> Lectures { get; set; }
    }
}
