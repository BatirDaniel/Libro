
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Entities
{
    public class UserTypes : IdentityRole
    {
        public string? UserType
        {
            get => this.Name;
            set => this.Name = value;
        }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
