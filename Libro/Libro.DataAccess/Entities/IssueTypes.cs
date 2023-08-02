namespace Libro.DataAccess.Entities
{
    public class IssueTypes
    {
        public string? Id { get; set; }
        public string? IssueLevel { get; set; }
        public string? ParentIssue { get; set; }
        public string? Name { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
