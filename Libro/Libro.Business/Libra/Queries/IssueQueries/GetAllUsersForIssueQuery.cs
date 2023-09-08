using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.IssueQueries
{
    public record GetAllUsersForIssueQuery 
        : IRequest<List<UsersForIssue>>;
    
    public record UsersForIssue(
        Guid Id,
        string Name);
}
