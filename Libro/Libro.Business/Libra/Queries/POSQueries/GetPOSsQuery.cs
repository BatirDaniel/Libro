
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using MediatR;

namespace Libro.Business.Libra.Queries.PosQueries
{
    public class GetPOSsQuery : IRequest<List<PosDTO>>
    { 
        public DataTablesParameters? Param { get; set; }
        public GetPOSsQuery(DataTablesParameters? param)
        {
            Param = param;
        }
    }
}
