using Libro.Business.Responses.IdentityResponses;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetAllUsersQuery : IRequest<List<UserResponse>> { }
}
