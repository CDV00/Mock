using Entities.Models;
using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class AppUser : UserBase
    {
        public string AvatarUrl { get; set; }
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        //public string Instroduction { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }

        public string RefreshToken { get; set; }
        public decimal Balance { get; set; } = 0;
        public string CodeNumber { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseCompletion> CourseCompletions { get; set; }
        public ICollection<Courses> Courses { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<LectureCompletion> LectureCompletions { get; set; }
        public ICollection<QuizCompletion> QuizCompletions { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<ShoppingCart> Carts { get; set; }
        public ICollection<SavedCourses> SavedCourses { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        //public ICollection<Notification> SentNotifications { get; set; }
    }
}
