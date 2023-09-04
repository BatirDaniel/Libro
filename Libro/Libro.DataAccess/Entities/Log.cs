namespace Libro.DataAccess.Entities
{
    public class Log
    {
        public Guid Id { get; set; }

        public Issue Issue { get; set; }
        public Guid IdIssue { get; set; }

        public User User { get; set; }
        public string IdUser { get; set; }

        public string Action { get; set; }
        public string? Notes { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
