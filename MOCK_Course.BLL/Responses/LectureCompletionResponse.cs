using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class LectureCompletionResponse
    {
        public Guid Id { get; set; }
        public LectureCompletionUser LectureCompUser { get; set; }
        public LectureCompletionLession Lecture { get; set; }
    }
    public class LectureCompletionUser
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class LectureCompletionLession
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalTime { get; set; }
    }
}
