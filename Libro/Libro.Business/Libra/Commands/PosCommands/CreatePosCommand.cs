using Libro.Business.Libra.DTOs.POSDTOs;
using MediatR;

namespace Libro.Business.Commands.PosCommands
{
    public class CreatePOSCommand : IRequest<string>
    {
        public CreatePOSDTO? PosDTO { get; set; }

        public CreatePOSCommand(CreatePOSDTO? posDTO)
        {
            PosDTO = posDTO;
        }
    }
}
