using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Course.DAL.Configuration
{
    internal class CoursesConfiguration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            // 1-n:category-course
            builder.HasOne(course => course.Category)
                .WithMany(cate => cate.Courses)
                .HasForeignKey(course => course.CategoryId);

            // 1-n:user-course
            builder.HasOne(c => c.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UserId);

            // 1-n:discount-course
            builder.HasOne(c => c.Discount)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.DiscountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Price).HasColumnType("money");
            builder.Property(c => c.DiscountPrice).HasColumnType("money");

            builder.HasQueryFilter(u => !u.IsDeleted);


        }
    }
}
