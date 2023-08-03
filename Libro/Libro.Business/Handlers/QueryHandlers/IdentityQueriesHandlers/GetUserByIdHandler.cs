using Libro.Business.Queries.IdentityQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, string>
    {
        public Task<string> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); //Need to be implemented :)
        }
    }
}
