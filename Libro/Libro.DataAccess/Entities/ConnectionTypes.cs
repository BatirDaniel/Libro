using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.DataAccess.Entities
{
    public class ConnectionTypes
    {
        public string? Id { get; set; }
        public string? ConnectionType { get; set; }

        public virtual ICollection<Pos> Pos { get; set; } = new List<Pos>();
    }
}
