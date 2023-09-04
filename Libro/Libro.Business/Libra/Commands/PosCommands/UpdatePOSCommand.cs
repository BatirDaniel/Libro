using Libro.Business.Libra.DTOs.POSDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Commands.PosCommands
{
    public class UpdatePOSCommand : IRequest<string>
    {
        public UpdatePOSDTO PosDTO { get; set; }

        public UpdatePOSCommand(UpdatePOSDTO posCommand)
        {
            PosDTO = posCommand;
        }
    }
}
