using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.POSDTOs
{
    public class PosDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
    }
}
