using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Driver.Common.Abstraction.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<T> GetAsync(params object[] keys);

        /// <summary>
        /// First Or Default
        /// </summary>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync( Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        Task<int> Count(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Max
        /// </summary>
        /// <returns></returns>
        Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        T Add(T newEntity);

        /// <summary>
        /// Add Range
        /// </summary>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Update
        /// </summary>
        void Update(T originalEntity, T newEntity);

        /// <summary>
        /// Update Async
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync(object id, T newEntity);

        /// <summary>
        /// Update Range
        /// </summary>
        void UpdateRange(IEnumerable<T> newEntities);

        /// <summary>
        /// Remove
        /// </summary>
        void Remove(T entity);

        /// <summary>
        /// Soft Remove
        /// </summary>
        void RemoveLogical(T entity);
        /// <summary>
        /// Remove With Condition
        /// </summary>
        void Remove(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Remove Range
        /// </summary>
        void RemoveRange(IEnumerable<T> entities);
    }
}
