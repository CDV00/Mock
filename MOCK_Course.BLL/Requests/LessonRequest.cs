using System;

namespace Course.BLL.Requests
{
    public class LessonForCreateRequest
    {
        //public Guid? SectionId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
    public class LessonForUpdateRequest : LessonForCreateRequest
    {
        public Guid Id { get; set; }
    }

    public class LessonDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
}
