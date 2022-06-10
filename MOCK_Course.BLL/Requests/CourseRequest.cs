using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class CourseBaseRequest
    {

    }
    public class CourseForCreateRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// What will students learn in your course?
        /// </summary>
        public string Learn { get; set; }
        public string Requirement { get; set; }
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; }

        public bool RequireLogin { get; set; }
        public bool RequireEnroll { get; set; }

        public bool IsFree { get; set; }

        public decimal Price { get; set; }
        //public decimal DiscountPrice { get; set; }

        public Guid CategoryId { get; set; }

        public IList<Guid> AudioLanguageIds { get; set; }
        public IList<Guid> CloseCaptionIds { get; set; }
        public IList<Guid> LevelIds { get; set; }

        public IList<SectionCreateRequest> Sections { get; set; }
    }
    public class CourseForUpdateRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// What will students learn in your course?*
        /// </summary>
        public string Learn { get; set; }
        public string Requirement { get; set; }

        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; }

        public bool RequireLogin { get; set; }
        public bool RequireEnroll { get; set; }

        public bool IsFree { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public IList<Guid> AudioLanguageIds { get; set; }
        public IList<Guid> CloseCaptionIds { get; set; }
        public IList<Guid> LevelIds { get; set; }

        public IList<SectionUpdateRequest> Sections { get; set; }
    }
}
