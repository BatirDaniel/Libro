using Libro.Business.Libra.DTOs.IdentityDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
