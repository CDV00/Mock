using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class AudioLanguageConfiguration : IEntityTypeConfiguration<AudioLanguage>
    {
        public void Configure(EntityTypeBuilder<AudioLanguage> builder)
        {
            builder.HasKey(al => new { al.CourseId, al.LanguageId });

            builder.HasOne<Courses>(c => c.Course)
    .WithMany(al => al.AudioLanguages)
    .HasForeignKey(c => c.CourseId);


            builder.HasOne<Language>(l => l.Language)
                .WithMany(al => al.AudioLanguages)
                .HasForeignKey(l => l.LanguageId);
        }
    }
}
