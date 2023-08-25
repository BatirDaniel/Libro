using Libro.Business.Commands.PosCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.POSCommandsHandlers
{
    public class CreatePOSHandler : IRequestHandler<CreatePOSCommand, string>
    {
        public PosManager _posManager { get; set; }
        public ILogger<CreatePOSHandler> _logger;

        public CreatePOSHandler(PosManager posManager, ILogger<CreatePOSHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<string> Handle(CreatePOSCommand request, CancellationToken cancellationToken)
        {
            return await _posManager.Create(request.PosDTO);
        }
    }
}
