using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, string?>
    {
        public IdentityManager _manager;
        public ILogger<RemoveUserHandler> _logger;

        public RemoveUserHandler(IdentityManager manager, ILogger<RemoveUserHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<string?> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            return await _manager.Remove(request);
        }
    }
}
