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
        }
    }
}
