﻿using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Responses.IdentityResponses;
using Libro.Business.Services;
using Libro.Business.Validators;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Helpers.ExpressionSuport;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
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
            AppSettings appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _user = user;
            _roleManager = roleManager;
            _appSettings = appSettings;
        }

        public async Task<string?> Create(AddUserCommand command)
        {
            command.Username = command.Username?.Trim();

            var _validation = new AddUserCommandValidator();
            var result = _validation.Validate(command);

            if (result != null)
                return result.ToString();

            User user = _mapperly.Map(command);
            user.Id = Guid.NewGuid().ToString();

            if (user.UserName != null)
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                    return "The provided username is already in use";

            if (user.Email != null)
                if (await _userManager.FindByEmailAsync(user.Email) != null)
                    return "The provided email is already in use";

            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();

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

        public async Task<List<RoleResponse>> GetAllRoles()
        {
            return (await _unitOfWork.UserTypes.FindAll<RoleResponse>(
                where : x => x.UserType != null)).ToList();
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

        public async Task<List<UserResponse>?> GetAutoCompleteUsers(string filter)
        {
            Expression<Func<User, bool>> expression = q => q.UserName != "admin@libro.com";

            if(filter != null)
            {
                Expression<Func<User, bool>> expression1 = 
                    q => q.UserName.Contains(filter) || q.Name.Contains(filter);

                expression = ExpressionCombiner.And(expression, expression1);
            }

            var users = (await _unitOfWork.Users.FindAll<UserResponse>(
                where: expression,
                skip: 0,
                take: int.MaxValue,
                select: x => _mapperly.Map(x))).ToList();

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
