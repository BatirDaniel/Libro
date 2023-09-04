using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Commands.IssueCommands
{
    public class DeleteIssueCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public DeleteIssueCommand(Guid id)
        {
            Id = id;
        }
    }
}
