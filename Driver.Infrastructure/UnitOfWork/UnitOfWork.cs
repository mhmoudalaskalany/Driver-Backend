using System;
using System.Data;
using Driver.Common.Abstraction.Repository;
using Driver.Common.Abstraction.UnitOfWork;
using Driver.Infrastructure.Repository;

namespace Driver.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IDbConnection _dbConnection;
        public IRepository<T> Repository { get; }
        public UnitOfWork(IDbTransaction dbTransaction, IDbConnection dbConnection)
        {
            _dbTransaction = dbTransaction;
            _dbConnection = dbConnection;
            Repository = new Repository<T>(_dbConnection, _dbTransaction);
        }


        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                _dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception e)
            {
                _dbTransaction.Rollback();
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_dbTransaction == null)
            {
                return;
            }

            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }


    }
}