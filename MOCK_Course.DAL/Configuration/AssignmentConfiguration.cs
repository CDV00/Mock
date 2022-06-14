using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            // 1-n:user-enrollment
            builder.HasOne(e => e.Section)
                .WithMany(u => u.Assignments)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
