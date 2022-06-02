using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class CourseLevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            //builder.HasKey(cl => new { cl.CourseId, cl.LevelId });

            //builder.HasOne(cl => cl.Course)
            //.WithMany(c => c.CourseLevels)
            //.HasForeignKey(cl => cl.CourseId);

            //builder.HasOne(cl => cl.Level)
            //    .WithMany(l => l.CourseLevels)
            //    .HasForeignKey(l => l.LevelId);


            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
