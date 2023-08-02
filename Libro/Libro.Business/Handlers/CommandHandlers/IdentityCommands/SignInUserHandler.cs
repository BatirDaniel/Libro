using Libro.Business.Commands.IdentityCommands;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Libro.Business.Handlers.CommandHandlers.IdentityCommands
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, string?>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<SignInUserHandler> _logger;
        public SignInUserHandler(IUnitOfWork unitOfWork, ILogger<SignInUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string?> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.Find<User>(u =>
                u.UserName == request.Username &&
                u.Password == request.Password);

            return user == null ? null : "Succes";
        }
    }
}
