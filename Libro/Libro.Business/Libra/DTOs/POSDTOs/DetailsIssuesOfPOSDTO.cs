using Libro.DataAccess.Entities;

namespace Libro.Business.Libra.DTOs.POSDTOs
{
    public class DetailsIssuesOfPOSDTO
    {
        public Guid Id { get; set; }
        public Pos POSName { get; set; }
        public User CreatedBy { get; set; }
        public string DateCreated { get; set; }
        public DataAccess.Entities.IssueTypes IssueType { get; set; }
        public Status Status { get; set; }
        public Role AssignedTo { get; set; }
        public string Memo { get; set; }
    }
}
