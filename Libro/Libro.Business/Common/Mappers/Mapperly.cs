using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.POSDTOs;
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
        public partial UpdateUserDTO Map(User user);
        public partial User Map(UpdateUserDTO command);
        public partial Role Map(IdentityRole role);
        public partial Issue Map(CreateIssueDTO issueDTO);
        public partial Issue Map(UpdateIssueDTO issueDTO);
        public partial UpdateIssueDTO Map(Issue issueDTO);
        public partial IssueDTO MapIssueDTO(Issue issueDTO);
        public partial Pos Map(CreatePOSDTO pos);
        public partial Pos Map(UpdatePOSDTO pos);
        public partial UpdatePOSDTO Map(Pos pos);
    }
}
