using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.Queries.IssueQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Handlers.QueryHandlers.IssueQueriesHandlers
{
    public class GetIssuesHandler : IRequestHandler<GetIssuesQuery, List<IssueR_DTO>>
    {
        public IssueManager _issueManager;
        public ILogger<GetIssuesHandler> _logger;

        public GetIssuesHandler(IssueManager issueManager, ILogger<GetIssuesHandler> logger = null)
        {
            _issueManager = issueManager;
            _logger = logger;
        }

        public async Task<List<IssueR_DTO>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
        {
            return await _issueManager.GetIssues(request.Param);
        }
    }
}
