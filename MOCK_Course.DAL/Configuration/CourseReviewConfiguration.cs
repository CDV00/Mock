using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class CourseReviewConfiguration : IEntityTypeConfiguration<CourseReview>
    {
        public void Configure(EntityTypeBuilder<CourseReview> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
