using Ordering.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Repositories.Base
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public interface IRepository<T> where T : Entity
#pragma warning restore CS0436 // Type conflicts with imported type
    {
      
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
