using Libro.DataAccess.Entities;
using Libro.Infrastructure.SystemConfiguration;
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Data
{
    public class SeedData
    {
        public static void Seed(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            SystemConfigurationService configurationService)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, configurationService);
        }

        private static void SeedUsers(UserManager<User> userManager, SystemConfigurationService configurationService)
        {
            if(userManager.FindByEmailAsync("admin@libro.com").Result == null)
            {
                var user = new User { Email = "admin@libro.com" };
                var userPassword = configurationService.settings.AdminPassword;

                var result = userManager.CreateAsync(user, userPassword).Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "Admin", "Technician","User" };

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
