using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Course.DAL.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // 1-n:category-category
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentId).
                OnDelete(DeleteBehavior.NoAction);

            //builder.Property(c => c.Id).
            builder.HasQueryFilter(u => !u.IsDeleted);

            //seedData
            builder.HasData(new Category(new Guid("9e47da69-3d3e-428d-a207-d53908753582"), "Development", null));
            builder.HasData(new Category(Guid.NewGuid(), "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
            builder.HasData(new Category(Guid.NewGuid(), "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
            builder.HasData(new Category(Guid.NewGuid(), "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));


            builder.HasData(new Category(new Guid("9e47da02-3d3e-428d-a207-d53908753582"), "Business", null));
            builder.HasData(new Category(Guid.NewGuid(), "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"))); 
            builder.HasData(new Category(Guid.NewGuid(), "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"))); 
            builder.HasData(new Category(Guid.NewGuid(), "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));

            builder.HasData(new Category(new Guid("9e47da02-3d3e-248d-a207-d53908753582"), "IT - SoftWare", null));
            builder.HasData(new Category(Guid.NewGuid(), "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
            builder.HasData(new Category(Guid.NewGuid(), "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
            builder.HasData(new Category(Guid.NewGuid(), "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
        }
    }
}
