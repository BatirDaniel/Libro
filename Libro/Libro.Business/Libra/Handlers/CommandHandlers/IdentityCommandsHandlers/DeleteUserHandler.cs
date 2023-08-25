using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, string?>
    {
        public IdentityManager _manager;
        public ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IdentityManager manager, ILogger<DeleteUserHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<string?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _manager.Delete(request);
        }
    }
}
