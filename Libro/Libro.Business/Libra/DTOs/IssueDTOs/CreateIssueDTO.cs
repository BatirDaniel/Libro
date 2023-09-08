using Libro.DataAccess.Entities;

namespace Libro.Business.Libra.DTOs.IssueDTOs
{
    public class CreateIssueDTO
    {
        public Guid Id { get; set; }
        public Guid IdPos { get; set; }
        public Guid IdType { get; set; }
        public Guid IdPriority { get; set; }
        public Guid IdStatus { get; set; }
        public string? Memo { get; set; }
        public Guid IdUserCreated { get; set; }
        public Guid IdUsersAsigned { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifDate { get; set; }
        public string Solution { get; set; }
    }
}
