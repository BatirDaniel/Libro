using Libro.Business.Libra.Commands.PosCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.POSCommandsHandlers
{
    public class UpdatePOSHandler : IRequestHandler<UpdatePOSCommand, string>
    {
        public PosManager _posManager { get; set; }
        public ILogger<UpdatePOSHandler> _logger;

        public UpdatePOSHandler(PosManager posManager, ILogger<UpdatePOSHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<string> Handle(UpdatePOSCommand request, CancellationToken cancellationToken)
        {
            return await _posManager.Update(request.PosDTO);
        }
    }
}
