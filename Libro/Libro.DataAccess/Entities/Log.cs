using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.DataAccess.Entities
{
    public class Log
    {
        public string? Id { get; set; }

        public Issue? Issue { get; set; }
        public string? IdIssue { get; set; }

        public User? User { get; set; }
        public string? IdUser { get; set; }

        public string? Action { get; set; }
        public string? Notes { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
