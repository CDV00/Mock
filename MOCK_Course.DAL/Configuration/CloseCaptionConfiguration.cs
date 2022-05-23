using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class CloseCaptionConfiguration : IEntityTypeConfiguration<CloseCaption>
    {
        public void Configure(EntityTypeBuilder<CloseCaption> builder)
        {
            builder.HasKey(al => new { al.CourseId, al.LanguageId });

            builder.HasOne<Courses>(c => c.Course)
    .WithMany(al => al.CloseCaptions)
    .HasForeignKey(c => c.CourseId);


            builder.HasOne<Language>(l => l.Language)
                .WithMany(al => al.CloseCaptions)
                .HasForeignKey(l => l.LanguageId);
        }
    }
}
