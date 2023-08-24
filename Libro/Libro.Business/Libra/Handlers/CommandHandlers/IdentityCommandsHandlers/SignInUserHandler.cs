using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using Libro.Business.Services;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommands
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, Tuple<List<Claim>?, string>>
    {
        public UserManager<User> _userManager;
        public IdentityService _identityService;
        public ILogger<SignInUserHandler> _logger;

        public SignInUserHandler(
            ILogger<SignInUserHandler> logger,
            UserManager<User> userManager,
            IdentityService identityService)
        {
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<Tuple<List<Claim>?, string>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByUsername(request.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new Tuple<List<Claim>?, string>(null, "Invalid username or password");

            if(user.IsArchieved)
                return new Tuple<List<Claim>?, string>(null, "This account is disabled");

            var claims = await _identityService.GenerateClaims(user);

            return new Tuple<List<Claim>?, string>(claims, null);
        }
    }
}
