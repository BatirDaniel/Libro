using Libro.Business.DTOs;
using Libro.Business.Responses.IdentityResponses;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
        public JqueryDatatableParam? param { get; set; }

        public GetUsersQuery(JqueryDatatableParam? param)
        {
            this.param = param;
        }
    }
}
