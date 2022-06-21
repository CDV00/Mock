using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.DAL.Models;

namespace Course.DAL.Configuration
{
    internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
