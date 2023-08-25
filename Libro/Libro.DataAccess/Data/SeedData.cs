using Libro.DataAccess.Entities;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Data
{
    public class SeedData
    {
        public static async Task Seed(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext,
            SystemConfigurationService systemConfigurationService)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, systemConfigurationService);
            SeedConnectionTypes(dbContext);
            SeedStatuses(dbContext);
            SeedIssueTypes(dbContext);
        }
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "Administrator", "Tehnical Group", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager, SystemConfigurationService configuration)
        {
            if (await userManager.FindByNameAsync("admin@libro") == null)
            {
                var user = new User { UserName = "admin@libro", Name = "Administrator", Email = "admin@libro.com" };
                var userPassword = configuration.AppSettings.AdminPassword;

                var result = await userManager.CreateAsync(user, userPassword);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
        private static void SeedIssueTypes(ApplicationDbContext dbContext)
        {
            int issueLevel = 0;
            (new List<string> { "Unstable Network", "Tehnical issue", "Printing Error", "Outdated Inventory" })
                .ForEach(x =>
                {
                    if(!dbContext.IssueTypes.Any(i => i.Name == x))
                    {
                        dbContext.IssueTypes.Add(new IssueTypes
                        {
                            Id = Guid.NewGuid().ToString(),
                            IssueLevel = issueLevel++.ToString(),
                            Name = x,
                            InsertDate = DateTime.Now
                        });
                    }
                });

            dbContext.SaveChanges();
        }
        private static void SeedStatuses(ApplicationDbContext dbContext)
        {
            (new List<string> { "New", "Assigned", "In progress", "Pending" })
                .ForEach(x =>
                {
                    if (!dbContext.Statuses.Any(s => s.Status_Name == x))
                    {
                        dbContext.Statuses.Add(new Status()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Status_Name = x
                        });
                    }
                });
            dbContext.SaveChanges();
        }
        private static void SeedConnectionTypes(ApplicationDbContext dbContext)
        {
            (new List<string> { "Remote", "Wi-Fi" })
                .ForEach(x =>
                {
                    if (!dbContext.ConnectionTypes.Any(c => c.ConnectionType == x))
                    {
                        dbContext.ConnectionTypes.Add(new ConnectionTypes
                        {
                            Id = Guid.NewGuid().ToString(),
                            ConnectionType = x
                        });
                    }
                });
            dbContext.SaveChanges();
        }

        // Configure logging to console
        //private static readonly ILogger _logger = LoggerFactory.Create(builder =>
        //{
        //    builder.AddConsole();
        //}).CreateLogger<SeedData>();
    }
}
