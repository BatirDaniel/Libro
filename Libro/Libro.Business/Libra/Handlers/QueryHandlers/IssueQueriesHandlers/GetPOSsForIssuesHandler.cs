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
    public class GetPOSsForIssuesHandler : IRequestHandler<GetPOSsForIssuesQuery, List<POSsForIssuesDTO>>
    {
        private IssueManager _manager;
        private ILogger<GetPOSsForIssuesHandler> _logger;

        public GetPOSsForIssuesHandler(ILogger<GetPOSsForIssuesHandler> logger, IssueManager manager = null)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task<List<POSsForIssuesDTO>> Handle(GetPOSsForIssuesQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetPOSsForIssues(request.param);
        }
    }
}
