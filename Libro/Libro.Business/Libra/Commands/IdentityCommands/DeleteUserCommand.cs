using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Commands.IdentityCommands
{
    public class DeleteUserCommand : IRequest<string>
    {
        public string? Id { get; set; }

        public DeleteUserCommand(string? id)
        {
            Id = id;
        }
    }
}
