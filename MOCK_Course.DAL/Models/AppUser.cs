using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class AppUser : UserBase
    {
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        public string Instroduction { get; set; }
        public string Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseCompletion> CourseCompletions { get; set; }
        public ICollection<Courses> Courses { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<LessonCompletion> LessonCompletions { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCart> Carts { get; set; }
    }
}
