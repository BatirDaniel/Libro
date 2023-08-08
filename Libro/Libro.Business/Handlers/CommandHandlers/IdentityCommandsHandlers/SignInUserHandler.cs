using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using Libro.Business.Services;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommands
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, Tuple<User?, string>>
    {
        public UserManager<User> _userManager;
        public IdentityService _identityService;
        public ILogger<SignInUserHandler> _logger;

        public SignInUserHandler(
            IdentityManager manager,
            ILogger<SignInUserHandler> logger,
            UserManager<User> userManager,
            IdentityService identityService)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<Tuple<User?, string>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByUsername(request.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, user.Password))
                return new Tuple<User?, string>(null, "Invalid Username or Password");

            return new Tuple<User?, string>(user, "Succes");
        }
    }
}
