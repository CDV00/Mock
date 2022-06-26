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
            //builder.HasOne(cr => cr.Enrollment)
            //       .WithOne(e => e.CourseReview)
            //       .HasForeignKey(cr => cr.);

            builder.HasQueryFilter(u => !u.IsDeleted);

        }
    }
}
