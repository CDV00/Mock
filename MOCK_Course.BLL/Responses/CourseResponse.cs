using Course.BLL.Requests;
using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class CourseResponse
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

        public bool RequireLogin { get; set; }
        public bool RequireEnroll { get; set; }

        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; }

        public IList<AudioLanguageCreateResponse> AudioLanguages { get; set; }
        public IList<CloseCaptionCreateResponse> CloseCaptions { get; set; }
    }
}
