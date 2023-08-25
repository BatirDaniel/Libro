using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.POSQueriesHandlers
{
    public class GetPOSByIdHandler : IRequestHandler<GetPOSByIdQuery, UpdatePOSDTO>
    {
        public PosManager _posManager;
        public ILogger<GetPOSByIdHandler> _logger;
        public GetPOSByIdHandler(PosManager posManager, ILogger<GetPOSByIdHandler> logger = null)
        {
            _posManager = posManager;
            _logger = logger;
        }

        public async Task<UpdatePOSDTO> Handle(GetPOSByIdQuery request, CancellationToken cancellationToken)
        {
            return await _posManager.GetPOSById(request.Id);
        }
    }
}
