using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.IdentityDTOs
{
    public class UserByIdResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
    }
}
