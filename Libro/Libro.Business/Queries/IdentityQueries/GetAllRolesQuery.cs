using Libro.Business.Responses.IdentityResponses;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetAllRolesQuery : IRequest<List<RoleResponse>> { }
}
