using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Driver.Common.Abstraction.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Driver.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync( Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }
            return await query.ToListAsync();
        }
        
        public async Task<int> Count(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.CountAsync() : await DbSet.CountAsync(predicate);

        public async Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await DbSet.MaxAsync(selector);
            return await DbSet.Where(predicate).MaxAsync(selector);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.AnyAsync() : await DbSet.AnyAsync(predicate);

        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }

        public async Task UpdateAsync(object id, T newEntity)
        {
            var originalEntity = await DbSet.FindAsync(id);
            if (originalEntity != null) Context.Entry((object)originalEntity).CurrentValues.SetValues(newEntity);
        }

        public void UpdateRange(IEnumerable<T> newEntities)
        {
            Context.UpdateRange(newEntities);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveLogical(T entity)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsDeleted");
            if (property != null) property.SetValue(entity, true);
            var id = type.GetProperty("Id")?.GetValue(entity);
            var original = DbSet.Find(id);
            Update(original, entity);
        }

        public async void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = await DbSet.FindAsync(predicate);
            if (objects != null) DbSet.RemoveRange(objects);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

    }
}
