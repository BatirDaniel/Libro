using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using MediatR;

namespace Libro.Business.Queries.IdentityQueries
{
    public class GetUserByIdQuery : IRequest<UpdateUserDTO>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
