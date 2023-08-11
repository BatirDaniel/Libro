using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NHibernate.Util;
using System.Security.Claims;
using System.Text;

namespace Libro.Business.Services
{
    public class IdentityService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _appSettings;
        private ClaimsPrincipal _user;
        public ClaimsPrincipal User => _user;

        public IdentityService(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork, 
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings,
            ClaimsPrincipal user)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _user = user;
        }

        public async Task<User> GetUserByUsername(string? username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<string> AssignRoles(User user, string? idUserType)
        {
            var currentRole = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByIdAsync(idUserType);
            if (currentRole == role)
                return null;

            var result = await _userManager.RemoveFromRolesAsync(
                user, await _userManager.GetRolesAsync(user));

            if (result.Errors.Count() > 0)
                return CheckResultForErrors(result);

            result = await _userManager.AddToRoleAsync(user, "User");

            if (idUserType != null)
            {
                List<string> actualRole = _roleManager
                    .Roles
                    .Where(q => idUserType.Contains(q.Name) && q.Name != "User")
                    .Select(q => q.Name)
                    .ToList();

                result = await _userManager.AddToRolesAsync(user, actualRole);
            }

            if (result.Errors.Count() > 0)
                return CheckResultForErrors(result);

            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> CreateUser(User user, string? password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return CheckResultForErrors(result);
        }

        private string CheckResultForErrors(IdentityResult result)
        {
            if (result.Errors.Count() > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                    stringBuilder.Append(error.Description + "\n");

                return stringBuilder.ToString();
            }
            return null;
        }

        public async Task<List<Claim>?> GenerateClaims(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.UserData, user.UserName)
            };

            List<string> userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            foreach (string role in userRoles)
            {
                userClaims.Add(new (ClaimTypes.Role, role));
            }

            return userClaims;
        }
    }
}
