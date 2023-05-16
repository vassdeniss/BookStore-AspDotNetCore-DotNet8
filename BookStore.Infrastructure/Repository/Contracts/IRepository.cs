using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? includes = null);

        Task<T?> GetByIdAsync(object id);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includes = null);

        Task AddAsync(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
