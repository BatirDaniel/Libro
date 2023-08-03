using Libro.Business.Commands.IdentityCommands;
using MediatR;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, string>
    {
        public Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); //Need to be implemented :)
        }
    }
}
