using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        public string Title { get; set; }

        public IList<LectureForCreateRequest> Lecture { get; set; }
    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }

        public IList<LectureForUpdateRequest> Lesson { get; set; }
    }
}
