using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.LogDTO
{
    //<summary>
    //LogDTO
    //<summary>
    public class LogDTO
    {
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
        public string User { get; set; }
        public string Notes { get; set; }
    }
}
