using System;
using Driver.Common.Abstraction.Repository;

namespace Driver.Common.Abstraction.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        /// <summary>
        /// Repository Instance In Base Service
        /// </summary>
        IRepository<T> Repository { get; }
        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        void Commit();
    }
}
