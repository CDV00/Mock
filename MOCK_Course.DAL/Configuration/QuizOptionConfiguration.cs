using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
    {
        public void Configure(EntityTypeBuilder<QuizOption> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
