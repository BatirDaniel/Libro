
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public DateTime? DateRegistered { get; set; }
        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
