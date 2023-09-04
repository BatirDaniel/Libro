using Libro.Business.Libra.DTOs.POSDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.POSQueries
{
    public class GetPOSDetailsQuery : IRequest<DetailsPOSDTO>
    {
        public Guid Id { get; set; }

        public GetPOSDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
