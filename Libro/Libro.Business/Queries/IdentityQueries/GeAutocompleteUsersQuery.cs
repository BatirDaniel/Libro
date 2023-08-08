using Libro.Business.Responses.IdentityResponses;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GeAutocompleteUsersQuery : IRequest<List<UserResponse>>
    {
        public string? filter;

        public GeAutocompleteUsersQuery(string? filter)
        {
            this.filter = filter;
        }
    }
}
