using MediatR;

namespace Libro.Business.Commands.IdentityCommands
{
    public class SignInUserCommand : IRequest<string>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
