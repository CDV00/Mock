using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class LectureCompletionConfiguration : IEntityTypeConfiguration<LectureCompletion>
    {
        public void Configure(EntityTypeBuilder<LectureCompletion> builder)
        {
            //builder.HasKey(c => new { c.LectureId, c.UserId });
            // 1-n:user-lessonCompletion
            builder.HasOne(l => l.User)
                .WithMany(u => u.LectureCompletions)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // 1-n:lecture-lessionCompletion
            // builder.HasOne(lc => lc.Lecture)
            //        .WithMany(l => l.LectureCompletions)
            //        .HasForeignKey(lc => lc.LectureId)
            //        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
