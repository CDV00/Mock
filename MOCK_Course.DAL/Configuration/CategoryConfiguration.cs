using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // 1-n:category-category
            builder.HasOne(c => c.Parentcategory)
                .WithMany(c => c.Categories)
                .HasForeignKey(c => c.ParentcategoryId).
                OnDelete(DeleteBehavior.NoAction);

            //builder.Property(c => c.Id).
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
