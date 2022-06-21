using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
