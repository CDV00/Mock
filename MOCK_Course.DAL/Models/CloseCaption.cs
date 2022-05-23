using System;

namespace Course.DAL.Models
{
    public class CloseCaption
    {
        public Guid CourseId { get; set; }
        public Courses Course { get; set; }

        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
