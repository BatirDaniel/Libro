using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.POSDTOs
{
    public class UpdatePOSDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Address { get; set; }
        public City? City { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public ConnectionTypes? ConnectionType { get; set; }
        public TimeSpan? MorningOpening { get; set; }
        public TimeSpan? MorningClosing { get; set; }
        public TimeSpan? AfternoonOpening { get; set; }
        public TimeSpan? AfternoonClosing { get; set; }
        public string? DaysClosed { get; set; }
    }
}
