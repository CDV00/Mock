using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class SectionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<LectureDTO> Lectures { get; set; }
    }

    public class SectionForDetailDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public int TotalTime { get; set; }
        //public int TotalLectures { get; set; }

        public IList<LectureForDetailDTO> Lectures { get; set; }
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

    public class LectureForDetailDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
}
