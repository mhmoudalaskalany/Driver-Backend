using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Driver.Common.Abstraction.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        Task<T> AddAsync(T newEntity);

        /// <summary>
        /// Update Async
        /// </summary>
        /// <returns></returns>
        Task<T> UpdateAsync( T entity);


        /// <summary>
        /// Remove
        /// </summary>
        Task<bool> DeleteAsync(T entity);


        /// <summary>
        /// Create Drivers Table
        /// </summary>
        void CreateDriversTable();

    }
}
