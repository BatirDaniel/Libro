using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Commands.PosCommands
{
    public class DeletePOSCommand : IRequest<string>
    {
        public string? Id { get; set; }

        public DeletePOSCommand(string? id)
        {
            Id = id;
        }
    }
}
