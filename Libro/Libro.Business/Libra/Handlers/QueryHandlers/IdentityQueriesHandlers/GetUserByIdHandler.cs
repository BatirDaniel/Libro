using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.DataAccess.Data;
using MediatR;

namespace Libro.Business.Handlers.QueryHandlers.IdentityQueriesHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UpdateUserCommand?>
    {
        public IdentityManager _manager;
        private ApplicationDbContext _context;

        public GetUserByIdHandler(IdentityManager manager, ApplicationDbContext context)
        {
            _manager = manager;
            _context = context;
        }

        public async Task<UpdateUserCommand?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetUserById(request.Id);
        }
    }
}
