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

            builder.Property(c => c.Price).HasColumnType("money");
            builder.Property(c => c.DiscountPrice).HasColumnType("money");

            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasData(new Courses()
            {
                Id = Guid.NewGuid(),
                Title = "New Course",
                UserId = Guid.Parse("9e59da69-3d3e-428d-a207-d53908752532"),
                CategoryId = Guid.Parse("29304F68-B4B7-4E3C-8151-04B03BA03CD2"),
                CourseLevel = Level.Beginer
            });
        }
    }
}
