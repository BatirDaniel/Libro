namespace Libro.DataAccess.Entities
{
    public class ConnectionTypes
    {
        public string? Id { get; set; }
        public string? ConnectionType { get; set; }

        public virtual ICollection<Pos> Pos { get; set; } = new List<Pos>();
    }
}
