using Renetdux.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly RenetduxDatabaseContext Context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(RenetduxDatabaseContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return await GetQuery(filter: filter, orderBy: orderBy, skip: skip, take: take, include: include).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return await GetQuery(orderBy: orderBy, skip: skip, take: take, include: include).ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return await GetQuery(filter: filter, orderBy: orderBy, include: include).FirstOrDefaultAsync();
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
                return await GetQuery().CountAsync();
            else
                return await GetQuery().CountAsync(filter);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return await GetQuery().AnyAsync(filter);
        }

        public virtual async Task<TEntity> FindAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return query;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IList<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IList<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            _dbSet.RemoveRange(entities);
        }

        protected string GetTableName<T>() where T : class
        {
            var mapping = Context.Model.FindEntityType(typeof(T)).Relational();
            return mapping.TableName;
        }
    }
}
