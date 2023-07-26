using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Infrastructure.Persistence.Configurations
{
    public class AppSettings
    {
        public string? JWT_Secret { get; set; }
        public int JWT_ExpireDays { get; set; }
        public string? AdminPassword { get; set; }
    }
}
