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
    public class GetIssuesQuery : IRequest<List<IssueR_DTO>>
    {
        public DataTablesParameters? Param { get; set; }

        public GetIssuesQuery(DataTablesParameters? param)
        {
            Param = param;
        }
    }
}
