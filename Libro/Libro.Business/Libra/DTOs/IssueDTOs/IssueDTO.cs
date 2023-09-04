using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.IssueDTOs
{
    public class IssueDTO
    {
        public Guid Id { get; set; }
        public string POSName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string IssueType { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string? Memo { get; set; }
        public string Priority { get; set; }
    }
}
