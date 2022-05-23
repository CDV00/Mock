using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Validation
            builder.HasQueryFilter(u => !u.IsDeleted);

            // 1-n:user-category
            builder.HasOne(u => u.Category)
                .WithMany(c => c.User)
                .HasForeignKey(u => u.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
