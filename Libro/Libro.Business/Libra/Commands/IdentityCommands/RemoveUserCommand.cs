using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Commands.IdentityCommands
{
    public class RemoveUserCommand : IRequest<string>
    {
        public string? Id { get; set; }

        public RemoveUserCommand(string? id)
        {
            Id = id;
        }
    }
}
