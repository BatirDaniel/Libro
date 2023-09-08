using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.IssueDTOs
{
    public class UpdateIssueDTO
    {
        public Guid Id { get; set; }
        public Pos Pos { get; set; }
        public Guid IdIssueType { get; set; }
        public string IdSubType { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string Memo { get; set; }
        public User User { get; set; }
        public User UserAsigned { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifDate { get; set; }
        public string Solution { get; set; }
    }
}
