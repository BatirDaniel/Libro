using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.Queries.PosQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.POSQueriesHandlers
{
    public class GetPOSsHandler : IRequestHandler<GetPOSsQuery, List<PosDTO>>
    {
        public PosManager _posManager;
        public ILogger<GetPOSsHandler> _logger;
        public GetPOSsHandler(PosManager posManager, ILogger<GetPOSsHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<List<PosDTO>> Handle(GetPOSsQuery request, CancellationToken cancellationToken)
        {
            return await _posManager.GetPOSs(request.Param);
        }
    }
}
