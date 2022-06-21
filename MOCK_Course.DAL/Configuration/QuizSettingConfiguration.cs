using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class QuizSettingConfiguration : IEntityTypeConfiguration<QuizSetting>
    {
        public void Configure(EntityTypeBuilder<QuizSetting> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
