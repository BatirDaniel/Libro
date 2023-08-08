using Libro.DataAccess.Entities;

namespace Libro.DataAccess.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<UserTypes> UserTypes { get; }
        IGenericRepository<Status> Status { get; }
        IGenericRepository<Pos> Pos { get; }
        IGenericRepository<Log> Logs { get; }
        IGenericRepository<IssueTypes> IssueTypes { get; }
        IGenericRepository<Issue> Issues { get; }
        IGenericRepository<ConnectionTypes> ConnectionTypes { get; }
        IGenericRepository<City> City { get; }

        Task Save();
    }
}
