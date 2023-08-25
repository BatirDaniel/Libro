using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.Queries.IssueQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.IssueQueriesHandlers
{
    public class GetIssueByIdHandler : IRequestHandler<GetIssueByIdQuery, UpdateIssueDTO>
    {
        public IssueManager _issueManager;
        public ILogger<GetIssueByIdHandler> _logger;

        public GetIssueByIdHandler(IssueManager issueManager, ILogger<GetIssueByIdHandler> logger = null)
        {
            _issueManager = issueManager;
            _logger = logger;
        }

        public async Task<UpdateIssueDTO> Handle(GetIssueByIdQuery request, CancellationToken cancellationToken)
        {
            return await _issueManager.GetIssueById(request.Id);
        }
    }
}
