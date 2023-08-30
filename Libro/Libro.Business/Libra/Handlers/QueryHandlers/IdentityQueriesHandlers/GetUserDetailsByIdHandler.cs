using Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.Queries.IdentityQueries;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Libra.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserDetailsByIdHandler : IRequestHandler<GetUserDetailsByIdQuery, DetailsUserDTO>
    {
        public IdentityManager _manager;
        public ILogger<GetUserByIdHandler> _logger;

        public GetUserDetailsByIdHandler(ILogger<GetUserByIdHandler> logger, IdentityManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task<DetailsUserDTO> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetUserDetails(request.Id);
        }
    }
}
