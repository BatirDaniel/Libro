
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Entities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
