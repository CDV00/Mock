using System;

namespace Course.DAL.Models
{
    public class LectureCompletion : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
