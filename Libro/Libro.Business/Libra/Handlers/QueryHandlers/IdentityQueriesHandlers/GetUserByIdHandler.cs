using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        public IdentityManager _manager;
        private ApplicationDbContext _context;

        public GetUserByIdHandler(IdentityManager manager, ApplicationDbContext context)
        {
            _manager = manager;
            _context = context;
        }

        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                return null;

            var user = await _context.Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return user ?? null;
        }
    }
}
