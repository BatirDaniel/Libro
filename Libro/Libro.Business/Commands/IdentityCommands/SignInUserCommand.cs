using Libro.DataAccess.Entities;
using MediatR;

namespace Libro.Business.Commands.IdentityCommands
{
    public class SignInUserCommand : IRequest<Tuple<User?, string>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
