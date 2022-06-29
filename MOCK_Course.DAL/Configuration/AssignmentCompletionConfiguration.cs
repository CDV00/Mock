using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class AssignmentCompletionConfiguration : IEntityTypeConfiguration<AssignmentCompletion>
    {
        public void Configure(EntityTypeBuilder<AssignmentCompletion> builder)
        {
            //builder.HasKey(c => new { c.AssignmentId, c.UserId });
            // 1-n:user-asignmentCompletion
            builder.HasOne(l => l.User)
                .WithMany(u => u.AssignmentCompletions)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
