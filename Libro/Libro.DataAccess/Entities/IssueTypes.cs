namespace Libro.DataAccess.Entities
{
    public class IssueTypes
    {
        public Guid Id { get; set; }
        public string IssueLevel { get; set; }
        public Guid ParentIssue { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
