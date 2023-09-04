using Libro.Business.Libra.DTOs.IssueDTOs;
using MediatR;

namespace Libro.Business.Libra.Commands.IssueCommands
{
    public class UpdateIssueCommand : IRequest<string>
    {
        public UpdateIssueDTO IssueDTO { get; set; }

        public UpdateIssueCommand(UpdateIssueDTO issueDTO)
        {
            IssueDTO = issueDTO;
        }
    }
}
