using Course.DAL.Data;
using Course.DAL.Models;
using CourseAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Migrations
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    try
                    {
                        var userManager = services.GetRequiredService<UserManager<AppUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                        await appContext.Database.MigrateAsync();
                        await Seed.SeedDataAsync(userManager, roleManager,appContext);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred during migration");
                    }
                }
            }

            return host;
        }
    }
}
