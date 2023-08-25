using Libro.Business.Commands.IssueCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.CommandHandlers.IssueCommandsHandlers
{
    public class CreateIssueHandler : IRequestHandler<CreateIssueCommand, string>
    {
        public IssueManager _issueManager;
        public ILogger<CreateIssueHandler> _logger;

        public CreateIssueHandler(IssueManager issueManager, ILogger<CreateIssueHandler> logger = null)
        {
            _issueManager = issueManager;
            _logger = logger;
        }

        public async Task<string> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueManager.Create(request.IssueDTO);
        }
    }
}
