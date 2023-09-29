using Libro.Business.Libra.DTOs.POSDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.POSQueries
{
    public record GetPOSDetailsByIssueQuery(Guid id) : IRequest<DetailsPOSDTO>
    {

    }
}
