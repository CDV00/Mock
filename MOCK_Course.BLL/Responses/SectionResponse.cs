using System;

namespace Course.BLL.Responses
{
    public class SectionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? TotalTime { get; set; }
        public Guid CourseId { get; set; }
    }
}
