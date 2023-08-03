using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUserByIdQuery : IRequest<string>
    {
        public string? Id { get; set; }

        public GetUserByIdQuery(string? id)
        {
            Id = id;
        }
    }
}
