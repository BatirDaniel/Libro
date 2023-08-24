using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Commands.IdentityCommands
{
    public class UpdateUserStatusCommand : IRequest<string>
    {
        public string Id { get; set; }
        public UpdateUserStatusCommand(string id)
        {
            Id = id;
        }
    }
}
