using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
    
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUsersQuery : IRequest<List<UserDTO>>
    {
        public DataTablesParameters? Param { get; set; }

        public GetUsersQuery(DataTablesParameters? param)
        {
            Param = param;
        }
    }
}
