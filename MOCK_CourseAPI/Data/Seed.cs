using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Data
{
    public static class Seed
    {
        public static async Task SeedDataAsync(UserManager<AppUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    AppDbContext appContext)
        {
            var AdminRoleId = Guid.NewGuid();
            var StudentRoleId = Guid.NewGuid();
            var InstructorRoleId = Guid.NewGuid();
            var adminId = new Guid("9e47da69-3d3e-428d-a395-d53908753582");

            if (!await userManager.Users.AnyAsync())
            {
                //var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
                //var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
                //if (users == null) return;

                var user = new AppUser
                {
                    Id = adminId,
                    UserName = "admin123",
                    Email = "admin123@gmail.com",
                    NormalizedEmail = "admin123@gmail.com",
                };

                var roles = new List<IdentityRole<Guid>>
                {
                new IdentityRole<Guid>{Name = "Admin",Id = AdminRoleId,NormalizedName="Admin"},
                new IdentityRole<Guid>{Name = "Instructor", Id = InstructorRoleId,NormalizedName="Instructor"},
                new IdentityRole<Guid>{Name = "Student", Id = StudentRoleId, NormalizedName = "Student" },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                //foreach (var user in users)
                //{
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "123");
                await userManager.AddToRoleAsync(user, "Admin");
                //}
            }

            if (!await appContext.Categories.AnyAsync())
            {
                // seed category
                await appContext.Categories.AddAsync(new Category(new Guid("9e47da69-3d3e-428d-a207-d53908753582"), "Development", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));

                await appContext.Categories.AddAsync(new Category(new Guid("9e47da02-3d3e-428d-a207-d53908753582"), "Business", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));

                await appContext.Categories.AddAsync(new Category(new Guid("9e47da02-3d3e-248d-a207-d53908753582"), "IT - SoftWare", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
            }

            if (!await appContext.Courses.AnyAsync())
            {
                // seed course
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = Guid.NewGuid(),
                    Title = "New Course",
                    UserId = adminId,
                    CategoryId = Guid.Parse("9e47da02-3d3e-248d-a207-d53908753582"),
                    //CourseLevel = Level.Beginer
                });
            }

            if (!await appContext.AudioLanguages.AnyAsync())
            {
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "English",
                });
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "Vietnamese",
                });
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "Japan",
                });
            }

            if (!await appContext.CloseCaptions.AnyAsync())
            {
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "English",
                });
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "Vietnamese",
                });
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "Japan",
                });
            }


            if (!await appContext.Levels.AnyAsync())
            {
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Beginner",
                });
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Intermediate",
                });
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Expert",
                });

            }

            await appContext.SaveChangesAsync();
        }
    }
}
