
using Microsoft.AspNetCore.Identity;

namespace Libro.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime? DateArchieved { get; set; }

        private bool isArchieved;

        public bool IsArchieved
        {
            get
            {
                return isArchieved;
            }
            set
            {
                isArchieved = value;
                DateArchieved = (value)
                    ? DateTime.Now
                    : null;
            }
        }
        public string Telephone { get; set; }
        public DateTime DateRegistered { get; set; }
        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
