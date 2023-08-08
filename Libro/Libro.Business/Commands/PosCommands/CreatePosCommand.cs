using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Commands.PosCommands
{
    public class CreatePosCommand
    {
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Address { get; set; }
        public string? IdCity { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? IdConnectionType { get; set; }
        public TimeSpan? MorningOpening { get; set; }
        public TimeSpan? MorningClosing { get; set; }
        public TimeSpan? AfternoonOpening { get; set; }
        public TimeSpan? AfternoonClosing { get; set; }
        public string? DaysClosed { get; set; }
        public DateTime InserDate { get; set; }
    }
}
