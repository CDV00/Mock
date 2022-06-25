using System;

namespace Course.DAL.Models
{
    public class LectureCompletion
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public int Time { get; set; }
    }
}
