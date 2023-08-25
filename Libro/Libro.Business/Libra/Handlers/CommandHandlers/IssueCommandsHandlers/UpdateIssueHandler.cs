using Libro.Business.Handlers.CommandHandlers.IdentityCommandsHandlers;
using Libro.Business.Libra.Commands.IssueCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.IssueCommandsHandlers
{
    public class UpdateIssueHandler : IRequestHandler<UpdateIssueCommand, string>
    {
        public IssueManager _isssueManager;
        public ILogger<DeleteUserHandler> _logger;

        public UpdateIssueHandler(IssueManager isssueManager, ILogger<DeleteUserHandler> logger = null)
        {
            _isssueManager = isssueManager;
            _logger = logger;
        }

        public async Task<string> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            return await _isssueManager.Update(request.IssueDTO);
        }
    }
}
