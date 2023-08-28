using Libro.Business.Libra.DTOs.IdentityDTOs;
using MediatR;

namespace Libro.Business.Commands.IdentityCommands
{
    public class UpdateUserCommand : IRequest<string>
    {
        public UpdateUserDTO? UserDTO { get; set; }

        public UpdateUserCommand(UpdateUserDTO? user)
        {
            UserDTO = user;
        }
    }
}
