using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class LectureAttachmentConfiguration : IEntityTypeConfiguration<LectureAttachment>
    {
        public void Configure(EntityTypeBuilder<LectureAttachment> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasOne(a => a.Lecture)
                   .WithMany(a => a.Attachments)
                   .HasForeignKey(a => a.LectureId);
        }
    }
}
