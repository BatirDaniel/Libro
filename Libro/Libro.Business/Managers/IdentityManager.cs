using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Services;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Helpers.ExpressionSuport;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHibernate.Util;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class IdentityManager
    {
        private ILogger<IdentityManager> _logger;
        private AppSettings _appSettings;
        private UserManager<User> _userManager;
        private IdentityService _identityService;
        private Mapperly _mapperly;
        private RoleManager<IdentityRole> _roleManager;
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;
        private ClaimsPrincipal _user;

        public IdentityManager(
            ILogger<IdentityManager> logger,
            UserManager<User> userManager,
            IdentityService identityService,
            Mapperly mapperly,
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings,
            ApplicationDbContext context = null)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _user = user;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<string?> Create(AddUserCommand command)
        {
            command.UserDTO.Username = command.UserDTO.Username?.Trim();

            User user = _mapperly.Map(command);
            user.DateRegistered = DateTime.Now;
            user.UserName = command.UserDTO.Username;

            if (user.UserName != null)
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                    return "The provided username is already in use";

            if (user.Email != null)
                if (await _userManager.FindByEmailAsync(user.Email) != null)
                    return "The provided email is already in use";

            string result = await _identityService.CreateUser(user, command.UserDTO.Password);
            if (result != null)
                return result;

            result = await _identityService.AssignRoles(user, command.UserDTO.Role.Id);
            if (result != null)
                return result;

            return null;
        }

        public async Task<string?> Delete(DeleteUserCommand model)
        {
            if (!await _unitOfWork.Users.isExists(x => x.Id == model.Id))
                return "Invalid id provided";

            var user = await GetFullUser(model.Id);

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string?> Update(UpdateUserCommand user)
        {
            string result = null;

            if (!await _unitOfWork.Users.isExists(x => x.Id == user.UserDTO.Id))
                return "Invalid id provided";

            var model = await _userManager.FindByIdAsync(user.UserDTO.Id);

            if (user.UserDTO.Password != null)
            {
                result = await _identityService.ResetPassword(model, user.UserDTO.Password);
                if (result != null)
                    return result;
            }

            model.UserName = user.UserDTO.Username;
            model.Name = user.UserDTO.Name;
            model.Telephone = user.UserDTO.Telephone;
            model.Email = user.UserDTO.Email;

            result = await _identityService.UpdateUserData(model);
            if (result != null)
                return result;

            result = await _identityService.AssignRoles(model, user.UserDTO.Role.Id);
            if (result != null)
                return result;

            return null;
        }
        private async Task<User?> GetFullUser(string? id)
        {
            var user = (await _unitOfWork.Users.Find<User>
                (where: x => x.Id == id,
                 include: x => x
                 .Include(x => x.Issues)
                 .Include(x => x.Logs))).FirstOrDefault();

            return user;
        }

        public async Task<List<UserDTO>?> GetUsers(DataTablesParameters? param)
        {
            string searchValue = param.Search.Value ?? "";

            Expression<Func<User, bool>> expression = q => q.UserName != "admin@libro";

            if(searchValue != "")
            {
                Expression<Func<User, bool>> expression1 = q => (q.Name.Contains(searchValue)
                || q.Name.Contains(searchValue)
                || q.UserName.Contains(searchValue)
                || q.Email.Contains(searchValue)
                || q.Telephone.Contains(searchValue)
                || _userManager.GetRolesAsync(q).Result.First().Contains(searchValue)
                || (q.IsArchieved ? "Archieved" : "Unarchieved").Contains(searchValue));

                expression = ExpressionCombiner.And(expression, expression1);
            }

            var users = await _context.Users
               .Where(expression)
               .OrderByExtension(param.Columns[param.Order[0].Column].Name, param.Order[0].Dir)
               .Skip(param.Start)
               .Take(param.Length)
               .Select(x => new UserDTO
               {
                   Id = x.Id,
                   Name = x.Name,
                   Email = x.Email,
                   Username = x.UserName,
                   Telephone = x.Telephone,
                   Role = _userManager.GetRolesAsync(x).Result.LastOrDefault(),
               }).ToListAsync();

            return users;
        }

        public async Task<UpdateUserDTO?> GetUserById(string? id)
        {
            var user = (await _unitOfWork.Users.Find<UpdateUserDTO>(
                where: x => x.Id == id,
                select: x => new UpdateUserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Telephone = x.Telephone,
                    Email = x.Email,
                    Username = x.UserName,
                })).FirstOrDefault();

            var origUser = _mapperly.Map(user);
            user.Role.Name = _userManager.GetRolesAsync(origUser).Result.First();
            user.Role.Id = await _roleManager.GetRoleIdAsync(user.Role);

            return user;
        }

        public async Task<string> UpdateUserStatus(string id)
        {
            if (!await _unitOfWork.Users.isExists(x => x.Id == id))
                return "Invalid id provided";

            var model = await _userManager.FindByIdAsync(id);
            model.IsArchieved = !model.IsArchieved;

            _unitOfWork.Users.Update(model);
            await _unitOfWork.Save();

            return null;
        }
    }
}
