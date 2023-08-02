using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Libro.DataAccess.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<TType>> FindAll<TType>(
            Expression<Func<T, bool>>? where = null,
            Expression<Func<T, TType>>? select = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            List<string>? includes = null,
            int? skip = null,
            int? take = null
            );

        Task<IList<TType>> Find<TType>(
           Expression<Func<T, bool>>? where = null,
           Expression<Func<T, TType>>? select = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<bool> isExists(Expression<Func<T, bool>>? expression = null);
        Task<int> Count(
            Expression<Func<T, bool>>? where = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
