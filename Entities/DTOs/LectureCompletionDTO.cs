using System;

namespace Course.BLL.Responses
{
    public class LectureCompletionDTO
    {
        public int Time { get; set; }
        public Guid UserId { get; set; }
        public Guid LectureId { get; set; }
    }
}
