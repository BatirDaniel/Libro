using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
    {
        public IdentityManager _manager;

        public GetUserByIdHandler(IdentityManager manager)
        {
            _manager = manager;
        }

        public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _manager.GetUserById(request.Id);
            return result ?? null;
        }
    }
}
