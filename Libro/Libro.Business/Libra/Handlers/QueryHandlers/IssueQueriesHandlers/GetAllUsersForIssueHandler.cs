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
    public class GetAllUsersForIssueHandler : IRequestHandler<GetAllUsersForIssueQuery, List<UsersForIssue>>
    {
        private IssueManager _manager;
        private ILogger<GetAllUsersForIssueHandler> _logger;

        public GetAllUsersForIssueHandler(ILogger<GetAllUsersForIssueHandler> logger, IssueManager manager = null)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task<List<UsersForIssue>> Handle(GetAllUsersForIssueQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.Run(() => _manager.GetUsersForIssue());
            return result;
        }
    }
}
