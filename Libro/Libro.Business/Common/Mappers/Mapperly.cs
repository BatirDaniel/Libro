using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;

namespace Libro.Infrastructure.Mappers
{
    //Create a mapper declaration as a partial class and apply the Riok.Mapperly.Abstractions.MapperAttribute attribute.
    [Mapper]
    public partial class Mapperly
    {
        //Create a mapper declaration as a partial class and apply the Riok.Mapperly.Abstractions.MapperAttribute attribute.
        public partial User Map(AddUserCommand command); //Mapping from AddUserCommand => User
        public partial UserResponse Map(User user);
        public partial User MapUpdateUserToUser(UpdateUserCommand command);
        public partial UserTypes Map(IdentityRole role);
    }
}
