using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.DAL.Models
{
    public class Courses : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// What will students learn in your course?*
        /// </summary>
        public string Learn { get; set; }
        public string Requirement { get; set; }
        //public Level CourseLevel { get; set; }
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// Intro Course overview provider type. (
        /// </summary>
        public string PreviewVideoUrl { get; set; }
        //public int View { get; set; } = 0;

        public bool RequireLogin { get; set; } = false;
        public bool RequireEnroll { get; set; } = false;

        public decimal Price { get; set; } = 0;

        public bool? IsFree { get; set; }

        public float SumRates { get; set; } = 0;
        public float AvgRate { get; set; } = 0;
        public int TotalReviews { get; set; } = 0;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<Discount> Discounts { get; set; }

        public ICollection<Section> Sections { get; set; }
        public ICollection<CourseCompletion> CourseCompletions { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ShoppingCart> Carts { get; set; }
        public ICollection<SavedCourses> SavedCourses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Level> Levels { get; set; }
        public ICollection<AudioLanguage> AudioLanguages { get; set; }
        public ICollection<CloseCaption> CloseCaptions { get; set; }
    }
}
