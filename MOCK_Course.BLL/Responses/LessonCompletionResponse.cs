using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class LessonCompletionResponse
    {
        public Guid Id { get; set; }
        public LessonCompletionUser LessonCompUser { get; set; }
        public LessonCompletionLession Lesson { get; set; }
    }
    public class LessonCompletionUser
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class LessonCompletionLession
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalTime { get; set; }
    }
}
