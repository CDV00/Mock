using System;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Course.DAL.Extensions
{
    public static class BuilderExtension
    {
        public static void ConfigTablesOfIdentity(this ModelBuilder builder)
        {
            const string claims = "AppUserClaims";
            const string roles = "AppUserRoles";
            const string userLogins = "AppUserLogins";
            const string roleClaims = "AppRoleClaims";
            const string userTokens = "AppUserTokens";

            builder.Entity<IdentityUserClaim<Guid>>().ToTable(claims);
            builder.Entity<IdentityUserRole<Guid>>().ToTable(roles).HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable(userLogins).HasKey(x => x.UserId);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable(roleClaims);
            builder.Entity<IdentityUserToken<Guid>>().ToTable(userTokens).HasKey(x => x.UserId);
        }

        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid> { Name = "Student", Id = Guid.Parse("9e59da69-3d3e-428d-a207-d53908753582") },
                new IdentityRole<Guid> { Name = "Instructor", Id = Guid.Parse("9e59da69-3d3e-428d-a207-d5390875f522") });

            var roleId = Guid.Parse("9e59da69-3d3e-428d-a207-d5390875f582");
            var adminId = Guid.Parse("9e59da69-3d3e-428d-a207-d53908752532");

            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = roleId,
                Name = "Admin",
            });

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin123",
                Email = "admin123@gmail.com",
                NormalizedEmail = "admin123@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1"),
                SecurityStamp = string.Empty,
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
