using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, string?>
    {
        public IdentityManager _manager;
        public ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IdentityManager manager, ILogger<UpdateUserHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<string?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _manager.Update(request);
        }
    }
}
