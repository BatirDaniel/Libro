using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Libro.Business.Handlers.QueryHandlers.IdentityHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<GetAllUsersHandler> _logger;
        public GetAllUsersHandler(IUnitOfWork unitOfWork, ILogger<GetAllUsersHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<UserResponse>?> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> expr = q => q.Email != null;

            var users = (await _unitOfWork.Users.FindAll<UserResponse>(
                where: expr,
                skip: 0,
                take: 7)).ToList();

            return users ?? null;
        }
    }
}
