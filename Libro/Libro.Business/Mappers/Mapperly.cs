using Libro.Business.Commands.IdentityCommands;
using Libro.DataAccess.Entities;
using Riok.Mapperly.Abstractions;

namespace Libro.Infrastructure.Mappers
{
    [Mapper]
    public partial class Mapperly
    {
        public partial User Map(AddUserCommand command);
    }
}
