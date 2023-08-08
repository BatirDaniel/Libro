using Libro.DataAccess.Entities;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Libro.DataAccess.Data
{
    public class SeedData
    {
        public static void Seed(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext,
            SystemConfigurationService systemConfigurationService)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, systemConfigurationService);
        }

        private static void SeedUsers(UserManager<User> userManager, SystemConfigurationService configuration)
        {
            if (userManager.FindByNameAsync("admin@libro").Result == null)
            {
                var user = new User { UserName = "admin@libro.com" };
                var userPassword = configuration.AppSettings.AdminPassword;

                var result = userManager.CreateAsync(user, userPassword).Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "Admin", "Technician", "User" };

            foreach (var r in roles)
            {
                if (roleManager.Roles == null || !roleManager.RoleExistsAsync(r).Result)
                {
                    var role = new IdentityRole { Name = r };
                    var result = roleManager.CreateAsync(role).Result;
                }
            }
        }
    }
}
