using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.POSQueriesHandlers
{
    public class GetIssuesOfPOSHandler : IRequestHandler<GetIssuesOfPOSQuery, List<DetailsIssuesOfPOSDTO>>
    {
        private PosManager _posManager;
        private ILogger<GetIssuesOfPOSHandler> _logger;

        public GetIssuesOfPOSHandler(ILogger<GetIssuesOfPOSHandler> logger, PosManager posManager = null)
        {
            _logger = logger;
            _posManager = posManager;
        }

        public async Task<List<DetailsIssuesOfPOSDTO>> Handle(GetIssuesOfPOSQuery request, CancellationToken cancellationToken)
        {
            return await _posManager.GetIssuesOfPOS(request.Param, request.Id);
        }
    }
}
