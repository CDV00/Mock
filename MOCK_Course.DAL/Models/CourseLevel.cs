using System;

namespace Course.DAL.Models
{
    public class CourseLevel : BaseEntity<Guid>
    {
        public Level Level { get; set; }
        public Courses Course { get; set; }

        public Guid LevelId { get; set; }
        public Guid CourseId { get; set; }
    }
}
