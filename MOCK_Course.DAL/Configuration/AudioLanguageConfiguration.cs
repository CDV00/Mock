using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class AudioLanguageConfiguration : IEntityTypeConfiguration<AudioLanguage>
    {
        public void Configure(EntityTypeBuilder<AudioLanguage> builder)
        {

            //        builder.HasMany(c => c.Courses)
            //.WithMany(al => al.AudioLanguages)
            //.ToTable("CourseAudioLanguage");

            //builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
