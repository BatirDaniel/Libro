using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Contracts
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
        IGenericRepository<Issue> ConnectionTypes { get; }
        IGenericRepository<Issue> City { get; }
    }
}
