using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.POSQueries
{
    public class GetIssuesOfPOSQuery : IRequest<List<DetailsIssuesOfPOSDTO>>
    {
        public string Id{ get; set; }
        public DataTablesParameters Param { get; set; }

        public GetIssuesOfPOSQuery(DataTablesParameters param, string id)
        {
            Param = param;
            Id = id;
        }
    }
}
