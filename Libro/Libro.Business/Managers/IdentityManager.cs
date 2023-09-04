using FluentNHibernate.Conventions;
using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Services;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
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

            User user = _mapperly.Map(command.UserDTO);
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
            if (!await _unitOfWork.Users.isExists(x => x.Id == model.Id.ToString()))
                return "Invalid id provided";

            var user = await GetFullUser(model.Id.ToString());

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string?> Update(UpdateUserCommand user)
        {
            string result = null;

            if (!await _unitOfWork.Users.isExists(x => x.Id == user.UserDTO.Id.ToString()))
                return "Invalid id provided";

            var model = await _userManager.FindByIdAsync(user.UserDTO.Id.ToString());

            if (user.UserDTO.Password != null)
            {
                result = await _identityService.ResetPassword(model, user.UserDTO.Password);
                if (result != null)
                    return result;
            }

            model.UserName = user.UserDTO.Username;
            model.Name = string.Join(" ", user.UserDTO.Firstname, user.UserDTO.Lastname);
            model.Telephone = user.UserDTO.Telephone;
            model.Email = user.UserDTO.Email;
            model.IsArchieved = (bool)user.UserDTO.IsArchieved;

            result = await _identityService.UpdateUserData(model);
            if (result != null)
                return result;

            result = await _identityService.AssignRoles(model, user.UserDTO.Role.Id);
            if (result != null)
                return result;

            return null;
        }
        private async Task<User> GetFullUser(string id)
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
            string searchValue = param.Search.Value?.ToLower() ?? "";

            Expression<Func<User, bool>> expression = q => q.UserName != "admin@libro";

            if (!string.IsNullOrEmpty(searchValue))
            {
                Expression<Func<User, bool>> expression1 = q =>
                    (q.Name.ToLower().Contains(searchValue) 
                    || q.UserName.ToLower().Contains(searchValue) 
                    || q.Email.ToLower().Contains(searchValue) 
                    || q.Telephone.ToLower().Contains(searchValue) 
                    || (q.IsArchieved ? "disabled" : "enabled").Contains(searchValue));

                expression = ExpressionCombiner.And(expression, expression1);
            }

            var users = _context.Users
                .Where(expression)
                .OrderByExtension(param.Columns[param.Order[0].Column].Name, param.Order[0].Dir)
                .Skip(param.Start)
                .Take(param.Length)
                .ToList();

            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userDTO = new UserDTO
                {
                    Id = Guid.Parse(user.Id),
                    Name = user.Name,
                    Email = user.Email,
                    Username = user.UserName,
                    Telephone = user.Telephone,
                    IsArchieved = user.IsArchieved,
                    Role = roles.FirstOrDefault()
                };

                userDTOs.Add(userDTO);
            }

            return userDTOs;
        }

        public async Task<UpdateUserDTO> GetUserById(Guid id)
        {
            var user = (await _unitOfWork.Users.Find<UpdateUserDTO>(
                where: x => x.Id == id.ToString(),
                select: x => new UpdateUserDTO
                {
                    Id = Guid.Parse(x.Id),
                    Name = x.Name,
                    Telephone = x.Telephone,
                    Email = x.Email,
                    Username = x.UserName,
                    IsArchieved = x.IsArchieved,
                })).FirstOrDefault();

            var origUser = _mapperly.Map(user);

            var userRoles = await _userManager.GetRolesAsync(origUser);

            if (userRoles.Any())
            {
                user.Role = new Role();
                user.Role.Name = userRoles.FirstOrDefault();

                var role = await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == user.Role.Name);
                if (role != null)
                {
                    user.Role.Id = role.Id;
                }
            }

            return user;
        }

        public async Task<DetailsUserDTO> GetUserDetails(Guid id)
        {
            var user = (await _unitOfWork.Users.Find<DetailsUserDTO>(
                where: x => x.Id == id.ToString(),
                include: x => x
                 .Include(x => x.Issues),
                select : x => new DetailsUserDTO
                {
                    Name = x.Name,
                    Email = x.Email,
                    Role = _userManager.GetRolesAsync(x).Result.FirstOrDefault(),
                    Joined = ((DateTime)x.DateRegistered).ToString("dd/MM/yyyy")
                })).FirstOrDefault();

            user.NumberOfIssuesAdded = await GetIssuesAdded(id);
            user.NumberOfIssuesAssigned = await GetIssuesAssigned(id);

            return user;
        }

        private async Task<int> GetIssuesAssigned(Guid id)
        {
            var result = (await _unitOfWork.Issues.FindAll<Issue>(
                where: x => x.IdAssigned == id.ToString())).Count();

            return result;
        }

        private async Task<int> GetIssuesAdded(Guid id)
        {
            var result = (await _unitOfWork.Issues.FindAll<Issue>(
               where: x => x.IdUserCreated == id.ToString())).Count();

            return result;
        }
    }
}
