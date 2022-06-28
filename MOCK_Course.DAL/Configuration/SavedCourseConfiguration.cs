using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class SavedCourseConfiguration : IEntityTypeConfiguration<SavedCourses>
    {
        public void Configure(EntityTypeBuilder<SavedCourses> builder)
        {
            builder.HasKey(c => new { c.CourseId, c.UserId });

            // 1-n:user-cart
            builder.HasOne(c => c.User)
                   .WithMany(u => u.SavedCourses)
                   .HasForeignKey(c => c.UserId);

            // 1-n:course-cart
            builder.HasOne(ct => ct.Course)
                   .WithMany(c => c.SavedCourses)
                   .HasForeignKey(ct => ct.CourseId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
