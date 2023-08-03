using Libro.Business.Queries.IdentityQueries;
using Libro.DataAccess.Entities;
using MediatR;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<UserTypes>>
    {
        async Task<List<UserTypes>> IRequestHandler<GetAllRolesQuery, List<UserTypes>>.Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); //Need to be implemented  :)
        }
    }
}
