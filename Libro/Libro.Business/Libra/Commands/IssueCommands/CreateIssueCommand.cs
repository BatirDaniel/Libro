using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Commands.IssueCommands
{
    public class CreateIssueCommand
    {
        public string? IdPos { get; set; }
        public string? IdType { get; set; }
        public string? IdSubType { get; set; }
        public string? Priority { get; set; }
        public string? IdStatus { get; set; }
        public byte[]? Memo { get; set; }
        public string? IdUserCreated { get; set; }
        public string? IdAssigned { get; set; }
        public string? Description { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifDate { get; set; }
        public string? Solution { get; set; }
    }
}
