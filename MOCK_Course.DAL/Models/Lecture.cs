using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Lecture : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<LectureCompletion> LectureCompletions { get; set; }
    }
}
