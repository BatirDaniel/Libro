using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Responses.IdentityResponses;
    
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
        public DataTablesParameters? param { get; set; }

        public GetUsersQuery(DataTablesParameters? param)
        {
            this.param = param;
        }
    }
}
