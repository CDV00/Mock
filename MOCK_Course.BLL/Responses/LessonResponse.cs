using System;

namespace Course.BLL.Responses
{
    public class LessonResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
}
