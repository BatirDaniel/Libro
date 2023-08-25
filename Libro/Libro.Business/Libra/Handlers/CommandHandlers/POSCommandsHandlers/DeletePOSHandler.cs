using Libro.Business.Libra.Commands.PosCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.POSCommandsHandlers
{
    public class DeletePOSHandler : IRequestHandler<DeletePOSCommand, string>
    {
        public PosManager _posManager;
        public ILogger<DeletePOSHandler> _logger;

        public DeletePOSHandler(PosManager posManager, ILogger<DeletePOSHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<string> Handle(DeletePOSCommand request, CancellationToken cancellationToken)
        {
            return await _posManager.Delete(request.Id); 
        }
    }
}
