using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Handlers.QueryHandlers.POSQueriesHandlers
{
    internal class GetPOSDetailsByIssueHandler : IRequestHandler<GetPOSDetailsByIssueQuery, DetailsPOSDTO>
    {
        private PosManager _posManager;
        private ILogger<GetPOSDetailsByIssueHandler> _logger;

        public GetPOSDetailsByIssueHandler(PosManager posManager, ILogger<GetPOSDetailsByIssueHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<DetailsPOSDTO> Handle(GetPOSDetailsByIssueQuery request, CancellationToken cancellationToken)
        {
            return await _posManager.GetPOSByIssue(request.id);
        }
    }
}
