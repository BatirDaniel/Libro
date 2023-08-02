using Libro.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Libro.DataAccess.Data
{
    public class DBDesigner : IDBDesigner
    {
        public void ConfigureModels(ModelBuilder modelBuilder)
        {
            SetColumnConstraint(modelBuilder);
            SetCustomRelations(modelBuilder);
        }

        private void SetCustomRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pos>()
                .HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.IdCity);

            modelBuilder.Entity<Pos>()
               .HasOne(x => x.ConnectionType)
               .WithMany()
               .HasForeignKey(x => x.IdConnectionType);

            modelBuilder.Entity<Issue>()
               .HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.IdUserCreated);

            modelBuilder.Entity<Issue>()
               .HasOne(x => x.Status)
               .WithMany()
               .HasForeignKey(x => x.IdStatus);

            modelBuilder.Entity<Issue>()
               .HasOne(x => x.IssueTypes)
               .WithMany()
               .HasForeignKey(x => x.IdType);

            modelBuilder.Entity<Issue>()
               .HasOne(x => x.Pos)
               .WithMany()
               .HasForeignKey(x => x.IdPos);

            modelBuilder.Entity<Issue>()
               .HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.IdAssigned);

            modelBuilder.Entity<Log>()
               .HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.IdUser);

            modelBuilder.Entity<Log>()
               .HasOne(x => x.Issue)
               .WithMany()
               .HasForeignKey(x => x.IdIssue);
        }

        private void SetColumnConstraint(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(x => x.CityName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Issue>()
                .Property(x => x.IdType)
                .IsRequired();

            modelBuilder.Entity<Issue>()
                .Property(x => x.IdAssigned)
                .IsRequired();

            modelBuilder.Entity<Pos>()
               .Property(x => x.IdCity)
               .IsRequired();

            modelBuilder.Entity<Pos>()
               .Property(x => x.IdConnectionType)
               .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
