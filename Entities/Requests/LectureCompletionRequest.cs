using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class LectureCompletionRequest
    {
        [Required]
        public Guid LectureId { get; set; }

        public int Time { get; set; } = 0;
    }
    public class LectureCompletionUpdateRequest : LectureCompletionRequest
    {
    }
}
