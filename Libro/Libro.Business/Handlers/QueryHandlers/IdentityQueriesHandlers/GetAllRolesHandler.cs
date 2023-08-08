using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleResponse>>
    {
        public IdentityManager _manager;
        public ILogger<GetAllRolesHandler> _logger;

        public GetAllRolesHandler(IdentityManager manager, ILogger<GetAllRolesHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<List<RoleResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _manager.GetAllRoles();
            return result ?? new List<RoleResponse>();
        }
    }
}
