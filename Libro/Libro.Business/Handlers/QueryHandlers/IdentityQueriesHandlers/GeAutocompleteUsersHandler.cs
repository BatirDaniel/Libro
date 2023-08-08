using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.QueryHandlers.IdentityHandlers
{
    public class GeAutocompleteUsersHandler : IRequestHandler<GeAutocompleteUsersQuery, List<UserResponse>?>
    {
        public IdentityManager _manager;
        public ILogger<GeAutocompleteUsersHandler> _logger;

        public GeAutocompleteUsersHandler(ILogger<GeAutocompleteUsersHandler> logger, IdentityManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task<List<UserResponse>?> Handle(GeAutocompleteUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _manager.GetAutoCompleteUsers(request.filter ?? "");
            return result ?? new List<UserResponse>();
        }
    }
}
