using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommands
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, string?>
    {
        public IdentityManager _manager;
        public ILogger<AddUserHandler> _logger;

        public AddUserHandler(IdentityManager manager, ILogger<AddUserHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<string?> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _manager.Create(request);
            return result != null ? result : null;
        }
    }
}
