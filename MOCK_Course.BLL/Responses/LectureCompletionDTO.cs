using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class LectureCompletionDTO
    {
        public Guid Id { get; set; }
        public LectureCompletionUserDTO User { get; set; }
        public LectureCompletionLessionDTO Lecture { get; set; }
    }
    public class LectureCompletionUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class LectureCompletionLessionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalTime { get; set; }
    }
}
