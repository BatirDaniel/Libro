using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UpdateUserDTO?>
    {
        public IdentityManager _manager;
        public ILogger<GetUserByIdHandler> _logger;

        public GetUserByIdHandler(IdentityManager manager, ILogger<GetUserByIdHandler> logger = null)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<UpdateUserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetUserById(request.Id);
        }
    }
}
