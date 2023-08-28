using Libro.DataAccess.Entities;
using MediatR;
using System.Security.Claims;

namespace Libro.Business.Commands.IdentityCommands
{
    public class SignInUserCommand : IRequest<Tuple<List<Claim>?, string>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
