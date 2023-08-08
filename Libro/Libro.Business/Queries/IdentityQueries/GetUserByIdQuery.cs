using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Entities;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public string? Id { get; set; }

        public GetUserByIdQuery(string? id)
        {
            Id = id;
        }
    }
}
