using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.IdentityDTOs
{
    public class DetailsUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Telephone { get; set; }
        public string Joined { get; set; }
        public int NumberOfIssuesAdded { get; set; }
        public int NumberOfIssuesAssigned { get; set; }
    }
}
