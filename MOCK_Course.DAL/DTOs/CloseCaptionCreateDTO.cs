using System;

namespace Course.BLL.Responses
{
    public class CloseCaptionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AudioLanguageDTO : CloseCaptionDTO { }
    public class CourseLevelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
