using System;
using System.ComponentModel.DataAnnotations.Schema;
using Course.DAL.Configuration;
using Course.DAL.Extensions;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        #region DbSet
        public DbSet<Courses> Courses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<CourseCompletion> CourseCompletions { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<LectureCompletion> LectureCompletions { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SavedCourses> SavedCourses { get; set; }

        public DbSet<AudioLanguage> AudioLanguages { get; set; }
        public DbSet<CloseCaption> CloseCaptions { get; set; }
        public DbSet<Level> Levels { get; set; }
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourseCompletionConfiguration());
            modelBuilder.ApplyConfiguration(new CourseReviewConfiguration());
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new LectureCompletionConfiguration());
            modelBuilder.ApplyConfiguration(new LectureConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new AudioLanguageConfiguration());
            modelBuilder.ApplyConfiguration(new CloseCaptionConfiguration());
            modelBuilder.ApplyConfiguration(new CourseLevelConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new SavedCourseConfiguration());

            modelBuilder.ConfigTablesOfIdentity();
            //modelBuilder.SeedData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors();
    }
}
