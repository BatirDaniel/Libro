using Libro.Business.Responses.IdentityResponses;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {

    }
}
