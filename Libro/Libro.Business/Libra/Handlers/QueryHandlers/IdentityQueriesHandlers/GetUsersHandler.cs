using Libro.Business.Managers;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;

namespace Libro.Business.Handlers.QueryHandlers.IdentityHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>?>
    {
        public IdentityManager _manager;
        public ILogger<GetUsersHandler> _logger;
        public IUnitOfWork _unitOfWork;
        public UserManager<User> _userManager;

        public GetUsersHandler(ILogger<GetUsersHandler> logger, IdentityManager manager, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _manager = manager;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<UserResponse>?> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _manager.GetUsers(request.param);
        }
    }
}
