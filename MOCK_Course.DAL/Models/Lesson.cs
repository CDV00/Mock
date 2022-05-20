using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Lesson : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public int TotalTime { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<LessonCompletion> LessonCompletions { get; set; }
    }
}
