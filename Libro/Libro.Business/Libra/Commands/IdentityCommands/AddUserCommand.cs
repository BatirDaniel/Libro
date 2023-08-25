using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Managers;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libro.Business.Commands.IdentityCommands
{
    public class AddUserCommand : IRequest<string>
    {
        public AddUserDTO? UserDTO { get; set; }

        public AddUserCommand(AddUserDTO? user)
        {
            UserDTO = user;
        }
    }
}
