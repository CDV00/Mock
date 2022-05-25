using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class LessonCompletionRequest
    {
        [Required]
        public Guid LessonId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public class LessonCompletionUpdateRequest : LessonCompletionRequest
    {
    } 
}
