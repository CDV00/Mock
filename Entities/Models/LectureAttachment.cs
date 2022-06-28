using System;

namespace Course.DAL.Models
{
    public class LectureAttachment : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }
        public string FileUrl { get; set; }
    }
}
