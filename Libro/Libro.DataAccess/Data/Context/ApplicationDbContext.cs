using Libro.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;

namespace Libro.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DBDesigner().ConfigureModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<Pos> Pos { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueTypes> IssueTypes { get; set; }
        public DbSet<ConnectionTypes> ConnectionTypes { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
