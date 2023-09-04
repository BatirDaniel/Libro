using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.POSQueriesHandlers
{
    public class GetPOSDetailsHandler : IRequestHandler<GetPOSDetailsQuery, DetailsPOSDTO>
    {
        public PosManager _posManager;
        public ILogger<GetPOSDetailsHandler> _logger;

        public GetPOSDetailsHandler(ILogger<GetPOSDetailsHandler> logger, PosManager posManager)
        {
            _logger = logger;
            _posManager = posManager;
        }

        public async Task<DetailsPOSDTO> Handle(GetPOSDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _posManager.GetPOSDetails(request.Id);
        }
    }
}
