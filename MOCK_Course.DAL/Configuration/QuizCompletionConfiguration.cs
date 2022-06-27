using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class QuizCompletionConfiguration : IEntityTypeConfiguration<QuizCompletion>
    {
        public void Configure(EntityTypeBuilder<QuizCompletion> builder)
        {
            builder.HasKey(c => new { c.QuizId, c.UserId });
            // 1-n:user-lessonCompletion
            builder.HasOne(l => l.User)
                .WithMany(u => u.QuizCompletions)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // 1-n:Quiz-lessionCompletion
            // builder.HasOne(lc => lc.Quiz)
            //        .WithOne(l => l.QuizCompletions)
            //        .HasForeignKey(lc => lc.QuizId)
            //        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
