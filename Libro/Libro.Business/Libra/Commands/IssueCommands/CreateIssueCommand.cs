using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Commands.IssueCommands
{
    public class CreateIssueCommand : IRequest<string>
    {
        public CreateIssueDTO? IssueDTO { get; set; }

        public CreateIssueCommand(CreateIssueDTO? issueDTO)
        {
            IssueDTO = issueDTO;
        }
    }
}
