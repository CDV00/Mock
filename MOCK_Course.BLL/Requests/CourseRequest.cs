using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class CourseRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// What will students learn in your course?*
        /// </summary>
        public string Learn { get; set; }
        public string Requirement { get; set; }
        public Level CourseLevel { get; set; }
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; }

        public bool RequireLogin { get; set; } = false;
        public bool RequireEnroll { get; set; } = false;

        public decimal Price { get; set; } = 0;
        public decimal DiscountPrice { get; set; } = 0;
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public ICollection<SectionRequest> SectionRequests { get; set; }
        public ICollection<AudioLanguageRequest> AudioLanguageRequests { get; set; }
        public ICollection<CloseCaptionRequest> CloseCaptionRequests { get; set; }
    }
    public class CourseUpdateRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// What will students learn in your course?*
        /// </summary>
        public string Learn { get; set; }
        public string Requirement { get; set; }
        public Level CourseLevel { get; set; }
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; }

        public bool RequireLogin { get; set; } = false;
        public bool RequireEnroll { get; set; } = false;

        public decimal Price { get; set; } = 0;
        public decimal DiscountPrice { get; set; } = 0;
        public Guid CategoryId { get; set; }

        public ICollection<SectionUpdateRequest> SectionRequests { get; set; }
        public ICollection<AudioLanguageRequest> AudioLanguageRequests { get; set; }
        public ICollection<CloseCaptionRequest> CloseCaptionRequests { get; set; }
    }
}
