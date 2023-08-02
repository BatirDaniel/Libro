using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Infrastructure.Persistence.SystemConfiguration
{
    public class AppSettings
    {
        public string? AdminPassword { get; set; }
        public string? Cookie_Name { get; set; }
        public int ExpireTimeSpan { get; set; }
        public bool SlidingExpiration { get; set; }
    }
}
