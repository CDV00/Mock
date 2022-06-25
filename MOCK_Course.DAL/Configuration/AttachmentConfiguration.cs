using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasOne(a => a.Assignment)
                .WithMany(a => a.Attachments)
                .HasForeignKey(a => a.AssignmentId);
        }
    }
}
