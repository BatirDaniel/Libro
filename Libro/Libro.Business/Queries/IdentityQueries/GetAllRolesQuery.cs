using Libro.DataAccess.Entities;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetAllRolesQuery : IRequest<List<UserTypes>> { }
}
