﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Driver.Common.Abstraction.Repository;

namespace Driver.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;
        public Repository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetAsync(int id)
        {
            var query = BuildSelectQuery();
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = BuildSelectAllQuery();
            return await _dbConnection.QueryAsync<T>(query);
        }


        public async Task<T> AddAsync(T entity)
        {
            var query = BuildInsertQuery();
            await _dbConnection.ExecuteAsync(query, entity);
            return entity;
        }

        public async Task<int> AddRangeAsync(List<T> entities)
        {
            var query = BuildInsertQuery();
            var rows = await _dbConnection.ExecuteAsync(query, entities);
            return rows;
        }


        public async Task<T> UpdateAsync(T entity)
        {
            var query = BuildUpdateQuery();
            await _dbConnection.ExecuteAsync(query, entity);
            return entity;
        }


        public async Task<bool> DeleteAsync(T entity)
        {
            var query = BuildDeleteQuery();
            var rows = await _dbConnection.ExecuteAsync(query);
            return rows > 0;
        }


        private string BuildInsertQuery()
        {
            var properties = GetProperties();
            var columns = string.Join(", ", properties.Where(p => p.Name != "Id").Select(p => p.Name));
            var values = string.Join(", ", properties.Where(p => p.Name != "Id").Select(p => "@" + p.Name));
            return $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({values})";
        }

        private string BuildUpdateQuery()
        {
            var properties = GetProperties().Where(p => !p.Name.Equals("Id"));
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            return $"UPDATE {typeof(T).Name}s SET {setClause} WHERE Id = @Id";
        }


        private string BuildSelectQuery()
        {
            return $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
        }

        private string BuildDeleteQuery()
        {
            return $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
        }

        private string BuildSelectAllQuery()
        {
            return $"SELECT * FROM {typeof(T).Name}s";
        }


        private IEnumerable<System.Reflection.PropertyInfo> GetProperties()
        {
            return typeof(T).GetProperties();
        }

        public int CreateDriversTable()
        {
            var result = _dbConnection.Execute(@"
            CREATE TABLE IF NOT EXISTS Drivers (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName NVARCHAR(250) NOT NULL,
                LastName NVARCHAR(250) NOT NULL,
                Email NVARCHAR(100) NOT NULL,
                PhoneNumber NVARCHAR(20) NOT NULL
            )");

            return result;
        }

    }
}
