using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        public Guid? CourseId { get; set; }
        [Required]
        public string Title { get; set; }

        public IList<LessonForCreateRequest> Lesson { get; set; }
    }
    public class SectionUpdateRequest
    {
        [Required]
        public string Title { get; set; }

        public IList<LessonForCreateRequest> Lesson { get; set; }
    }
}
