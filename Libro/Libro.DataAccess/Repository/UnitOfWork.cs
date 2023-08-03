﻿using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;

namespace Libro.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private IGenericRepository<User>? _users;

        private IGenericRepository<UserTypes>? _userTypes;

        private IGenericRepository<Status>? _status;

        private IGenericRepository<Pos>? _pos;

        private IGenericRepository<Log>? _logs;

        private IGenericRepository<IssueTypes>? _issueTypes;

        private IGenericRepository<Issue>? _issues;

        private IGenericRepository<ConnectionTypes>? _connectionTypes;

        private IGenericRepository<City>? _city;


        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<UserTypes> UserTypes => _userTypes ??= new GenericRepository<UserTypes>(_context);
        public IGenericRepository<Status> Status => _status ??= new GenericRepository<Status>(_context);
        public IGenericRepository<Pos> Pos => _pos ??= new GenericRepository<Pos>(_context);
        public IGenericRepository<Log> Logs => _logs ??= new GenericRepository<Log>(_context);
        public IGenericRepository<IssueTypes> IssueTypes => _issueTypes ??= new GenericRepository<IssueTypes>(_context);
        public IGenericRepository<Issue> Issues => _issues ??= new GenericRepository<Issue>(_context);
        public IGenericRepository<ConnectionTypes> ConnectionTypes => _connectionTypes ??= new GenericRepository<ConnectionTypes>(_context);
        public IGenericRepository<City> City => _city ??= new GenericRepository<City>(_context);

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}