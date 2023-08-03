using Libro.Business.Commands.IdentityCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, string>
    {
        public Task<string> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); //Need to be implemented :)
        }
    }
}
