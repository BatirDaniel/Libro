namespace Libro.DataAccess.Entities
{
    public class Issue
    {
        public string? Id { get; set; }

        public Pos? Pos { get; set; }
        public string? IdPos { get; set; }

        public IssueTypes? IssueTypes { get; set; }
        public string? IdType { get; set; }

        public string? IdSubType { get; set; }
        public string? Priority { get; set; }

        public Status? Status { get; set; }
        public string? IdStatus { get; set; }

        public byte[]? Memo { get; set; }

        public User? User { get; set; }
        public string? IdUserCreated { get; set; }

        public User? UserAsigned { get; set; }
        public string? IdAssigned { get; set; }

        public string? Description { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifDate { get; set; }
        public string? Solution { get; set; }

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
