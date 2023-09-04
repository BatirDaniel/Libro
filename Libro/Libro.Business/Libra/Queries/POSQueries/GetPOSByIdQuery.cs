using Libro.Business.Libra.DTOs.POSDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.POSQueries
{
    public class GetPOSByIdQuery : IRequest<UpdatePOSDTO>
    {
        public Guid Id { get; set; }

        public GetPOSByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
