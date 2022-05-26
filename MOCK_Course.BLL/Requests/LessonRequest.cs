using System;

namespace Course.BLL.Requests
{
    public class LessonCreateRequest
    {
        public Guid SectionId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
    public class LessonUpdateRequest : LessonCreateRequest
    {
        public Guid Id { get; set; }
    }
}
