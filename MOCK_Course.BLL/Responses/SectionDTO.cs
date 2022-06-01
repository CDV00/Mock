using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Responses
{
    public class SectionDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }

        public IList<LectureDTO> Lesson { get; set; }
    }

    public class LectureDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
}
