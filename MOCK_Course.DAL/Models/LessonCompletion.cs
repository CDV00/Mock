using System;

namespace Course.DAL.Models
{
    public class LessonCompletion : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
