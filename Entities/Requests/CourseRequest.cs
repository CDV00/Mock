using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class CourseBaseRequest
    {

    }
    public class CourseForCreateRequest
    {
        public string Title { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string Description { get; set; } = "";
        /// <summary>
        /// What will students learn in your course?
        /// </summary>
        public string Learn { get; set; } = "";
        public string Requirement { get; set; } = "";
        public string ThumbnailUrl { get; set; } = "";
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; } = "";

        public bool RequireLogin { get; set; } = false;
        public bool RequireEnroll { get; set; } = false;

        public bool IsFree { get; set; } = false;
        public bool IsActive { get; set; } = false;

        public decimal Price { get; set; } = 0;
        public Guid? CategoryId { get; set; } = null;
        public List<Guid> AudioLanguageIds { get; set; }
        public List<Guid> CloseCaptionIds { get; set; }
        public List<Guid> LevelIds { get; set; }

        public List<SectionCreateRequest> Sections { get; set; }
    }
    public class CourseForUpdateRequest
    {
        public string Title { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string Description { get; set; } = "";
        /// <summary>
        /// What will students learn in your course?*
        /// </summary>
        public string Learn { get; set; } = "";
        public string Requirement { get; set; } = "";

        public string ThumbnailUrl { get; set; } = "";
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; } = "";

        public bool RequireLogin { get; set; } = false;
        public bool RequireEnroll { get; set; } = false;

        public bool IsFree { get; set; } = false;
        public bool IsActive { get; set; } = false;

        public decimal Price { get; set; } = 0;
        public Status Status { get; set; } = Status.Review;

        public Guid? CategoryId { get; set; } = null;
        public List<Guid> AudioLanguageIds { get; set; }
        public List<Guid> CloseCaptionIds { get; set; }
        public List<Guid> LevelIds { get; set; }

        public List<SectionUpdateRequest> Sections { get; set; }
    }
    public class CourseStatusUpdateRequest
    {
        public Guid CourseId { get; set; }
        public int status { get; set; }
    }
}
