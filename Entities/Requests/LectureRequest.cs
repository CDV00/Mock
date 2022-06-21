using System;

namespace Course.BLL.Requests
{
    public class LectureForCreateRequest
    {
        //public Guid? SectionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string VideoExternalUrl { get; set; }
        public string VideoPoster { get; set; }
        public string FileUrl { get; set; }
        public int Index { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
    }
    public class LectureForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string VideoExternalUrl { get; set; }
        public string VideoPoster { get; set; }
        public string FileUrl { get; set; }
        public int Index { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = true;
    }
}
