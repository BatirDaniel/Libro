using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Services;
using Libro.Business.Validators;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Libro.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class IdentityManager : EntityManager
    {
        public ILogger<IdentityManager> _logger;
        public UserManager<User> _userManager;
        public IdentityService _identityService;
        public Mapperly _mapperly;
        public IdentityManager(
            UnitOfWork unitOfWork,
            ClaimsPrincipal user,
            ILogger<IdentityManager> logger,
            UserManager<User> userManager,
            IdentityService identityService,
            Mapperly mapperly) : base(unitOfWork, user)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
            _mapperly = mapperly;
        }

        public async Task<string> Create(AddUserCommand command)
        {
            command.Username = command.Username?.Trim();

            var _validation = new AddUserCommandValidator();
            var result = _validation.Validate(command);

            if (result != null)
                return result.ToString();

            User user = _mapperly.Map(command);

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
    }
}
