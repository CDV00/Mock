using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class CourseLevelConfiguration : IEntityTypeConfiguration<CourseLevel>
    {
        public void Configure(EntityTypeBuilder<CourseLevel> builder)
        {
            builder.HasKey(cl => new { cl.CourseId, cl.LevelId });

            builder.HasOne(cl => cl.Course)
            .WithMany(c => c.CourseLevels)
            .HasForeignKey(cl => cl.CourseId);

            builder.HasOne(cl => cl.Level)
                .WithMany(l => l.CourseLevels)
                .HasForeignKey(l => l.LevelId);
        }
    }
}
