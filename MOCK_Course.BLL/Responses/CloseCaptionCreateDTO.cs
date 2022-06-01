using Course.BLL.Requests;
using System;

namespace Course.BLL.Responses
{
    public class CloseCaptionDTO
    {
        public Guid LanguageId { get; set; }
    }

    public class AudioLanguageDTO : CloseCaptionDTO { }
    public class CourseLevelDTO
    {
        public Guid LevelId { get; set; }
    }

}
