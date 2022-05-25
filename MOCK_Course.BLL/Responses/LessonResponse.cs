using Course.BLL.Requests;
using System;

namespace Course.BLL.Responsesnamespace
{
    public class LessonResponse : LessonCreateRequest
    {
        public Guid Id { get; set; }
    }
}
