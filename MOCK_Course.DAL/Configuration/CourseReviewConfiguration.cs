using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class CourseReviewConfiguration : IEntityTypeConfiguration<CourseReview>
    {
        public void Configure(EntityTypeBuilder<CourseReview> builder)
        {
            // 1-n:enrollment-courseReview
            builder.HasOne(cr => cr.Enrollment)
                .WithMany(e => e.CourseReviews)
                .HasForeignKey(cr => cr.EnrollmentId);

            builder.HasQueryFilter(u => !u.IsDeleted);

        }
    }
}
