using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.DataAccess.Entities
{
    public class City
    {
        public string? Id { get; set; }
        public string? CityName { get; set; }

        public virtual ICollection<Pos> Pos { get; set; } = new List<Pos>();
    }
}
