using Course.BLL.Requests;
using System;

namespace Course.BLL.Responses
{
    public class LessonResponse : LessonCreateRequest
    {
        public Guid Id { get; set; }
    }
}
