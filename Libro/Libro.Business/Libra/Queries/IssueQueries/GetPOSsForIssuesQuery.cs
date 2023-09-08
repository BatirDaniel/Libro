using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.IssueQueries
{
    public record GetPOSsForIssuesQuery(DataTablesParameters? param) 
        : IRequest<List<POSsForIssuesDTO>>;

    public record POSsForIssuesDTO(
        Guid Id, 
        string Name, 
        string Telephone, 
        string Cellphone, 
        string Address);
}
