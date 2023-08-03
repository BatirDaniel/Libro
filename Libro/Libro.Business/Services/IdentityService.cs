using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NHibernate.Util;
using System.Security.Claims;
using System.Text;

namespace Libro.Business.Services
{
    public class IdentityService
    {
        public UserManager<User> _userManager;
        public UnitOfWork _unitOfWork;

        public IdentityService(UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByUsername(string? username)
        {
            return await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);
        }

        public async Task<string> AssignRoles(User user, string? idUserType)
        {
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
    }
}
