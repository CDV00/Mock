using Course.BLL.DTO;
using Course.DAL.DTOs;
using Course.DAL.Models;
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
        public bool IsSave { get; set; }
        public bool IsEnroll { get; set; }
        public bool IsPurchased { get; set; }
        public float MyRating { get; set; }

        public decimal Price { get; set; }
        public float AvgRate { get; set; } = 0;
        public int TotalEnrolls { get; set; } = 0;
        public int TotalTime { get; set; } = 0;
        // Percent completion
        public float PercentCompletion { get; set; }
        // isEnroll

        public Status Status { get; set; }
        //public int TotalSection { get; set; }
        //public int TotalOrder { get; set; }

        //public float PercentComplete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public UserDTO User { get; set; }
        public CategoryDTO Category { get; set; }
        public List<DiscountDTO> Discounts { get; set; }

        public IList<AudioLanguageDTO> AudioLanguages { get; set; }
        public IList<CloseCaptionDTO> CloseCaptions { get; set; }
        public IList<CourseLevelDTO> Levels { get; set; }

        public IList<SectionDTO> Sections { get; set; }
    }
}
