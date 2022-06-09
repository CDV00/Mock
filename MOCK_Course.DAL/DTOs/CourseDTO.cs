using Course.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public UserDTO User { get; set; }
        public CategoryDTO Category { get; set; }
        public List<DiscountDTO> Discounts { get; set; }

        public IList<AudioLanguageDTO> AudioLanguages { get; set; }
        public IList<CloseCaptionDTO> CloseCaptions { get; set; }
        public IList<CourseLevelDTO> CourseLevels { get; set; }

        public IList<SectionDTO> Sections { get; set; }
    }

    public class CourseForDetailDTO
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

        public decimal Price { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public CourseCategoryDTO Category { get; set; }

        public int Rate { get; set; }
        public int TotalEnroll { get; set; }
        public bool IsEnroll { get; set; }
        public bool IsOrder { get; set; }
        public bool IsSave { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public int Shares { get; set; }
        public int View { get; set; }

        public CourseDetailUserDTO User { get; set; }

        public IList<AudioLanguageDTO> AudioLanguages { get; set; }
        public IList<CloseCaptionDTO> CloseCaptions { get; set; }

        public IList<SectionForDetailDTO> Sections { get; set; }
    }

    public class CourseDetailUserDTO
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public bool IsSubscription { get; set; }
    }
}
