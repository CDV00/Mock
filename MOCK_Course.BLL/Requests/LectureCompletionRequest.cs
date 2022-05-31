using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class LectureCompletionRequest
    {
        [Required]
        public Guid LectureId { get; set; }
    }
    public class LectureCompletionUpdateRequest : LectureCompletionRequest
    {
    }
}
