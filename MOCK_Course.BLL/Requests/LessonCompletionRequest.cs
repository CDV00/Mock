using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class LessonCompletionRequest
    {
        [Required]
        public Guid LessonId { get; set; }
    }
    public class LessonCompletionUpdateRequest : LessonCompletionRequest
    {
    } 
}
