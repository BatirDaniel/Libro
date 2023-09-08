using Libro.DataAccess.Entities;

namespace Libro.DataAccess.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Role> UserTypes { get; }
        IGenericRepository<Status> Statuses { get; }
        IGenericRepository<Priority> Priorities { get; }
        IGenericRepository<Pos> POSs { get; }
        IGenericRepository<Log> Logs { get; }
        IGenericRepository<IssueTypes> IssueTypes { get; }
        IGenericRepository<Issue> Issues { get; }
        IGenericRepository<ConnectionTypes> ConnectionTypes { get; }
        IGenericRepository<City> Cities { get; }

        Task Save();
    }
}
