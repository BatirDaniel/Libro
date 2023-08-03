using MediatR;

namespace Libro.Business.Commands.IdentityCommands
{
    public class AddUserCommand : IRequest<string>
    {
        public string? Id { get; set; }
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public string? IdUserType { get; set; }
    }
}
