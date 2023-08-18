using Libro.Business.Commands.IdentityCommands;
using Libro.Business.DTOs;
using Libro.Business.Responses.IdentityResponses;
using Libro.Business.Services;
using Libro.Business.Validators;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Helpers.ExpressionSuport;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings;
using Microsoft.AspNetCore.Http;
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
        private ClaimsPrincipal _user;

        public IdentityManager(
            ILogger<IdentityManager> logger,
            UserManager<User> userManager,
            IdentityService identityService,
            Mapperly mapperly,
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _user = user;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        public async Task<string?> Create(AddUserCommand command)
        {
            command.Username = command.Username?.Trim();

            var _validation = new AddUserCommandValidator();
            var validate = _validation.Validate(command);

            if (!validate.IsValid)
                return validate.ToString();

            User user = _mapperly.Map(command);
            user.DateRegistered = DateTime.Now;
            user.UserName = command.Username;

            if (user.UserName != null)
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                    return "The provided username is already in use";

            if (user.Email != null)
                if (await _userManager.FindByEmailAsync(user.Email) != null)
                    return "The provided email is already in use";

            string result = await _identityService.CreateUser(user, user.Password);
            if (result != null)
                return result;

            result = await _identityService.AssignRoles(user, command.IdUserType);
            if (result != null)
                return result;

            return null;
        }

        public async Task<string?> Remove(RemoveUserCommand request)
        {
            var user = await GetFullUser(request.Id);

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string?> Update(UpdateUserCommand request)
        {
            var _user = _mapperly.Map(request);

            var _validator = new AddUserCommandValidator();
            var result = _validator.Validate(_user);

            if(!result.IsValid)
                return result.ToString();

            User user = _mapperly.Map(_user);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();

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

        public async Task<List<UserResponse>?> GetUsers(JqueryDatatableParam param)
        {
            Expression<Func<User, bool>> expression = q => q.UserName != "admin@libro";

            var users = (await _unitOfWork.Users.FindAll<UserResponse>(
                where: expression,
                skip: 0,
                take: int.MaxValue,
                select: x => new UserResponse
                {
                    Username = x.UserName,
                    Telephone = x.Telephone,
                    Email = x.Email,
                    Name = x.Name,
                    Role = _userManager.GetRolesAsync(x).Result.LastOrDefault(),
                })).ToList();

            return users;
        }

        public async Task<UserResponse?> GetUserById(string? id)
        {
            if (!string.IsNullOrEmpty(id))
                return null;

            var user = (await _unitOfWork.Users.Find<UserResponse>(
                where: x => x.Id == id)).FirstOrDefault();

            return user;
        }
    }
}
