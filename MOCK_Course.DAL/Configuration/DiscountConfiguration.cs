using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.DAL.Configuration
{
    internal class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {

            builder.Property(c => c.DiscountPercent).HasColumnType("decimal(5,2)");

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
