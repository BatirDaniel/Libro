using Libro.Business.Libra.Commands.IssueCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.IssueCommandsHandlers
{
    public class DeleteIssueHandler : IRequestHandler<DeleteIssueCommand, string>
    {
        public IssueManager _issueManager;
        public ILogger<DeleteIssueHandler> _logger;

        public DeleteIssueHandler(IssueManager issueManager, ILogger<DeleteIssueHandler> logger = null)
        {
            _issueManager = issueManager;
            _logger = logger;
        }

        public async Task<string> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueManager.Delete(request.Id);
        }
    }
}
