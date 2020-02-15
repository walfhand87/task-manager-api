using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TaskManager.Shared.DataAccess.Abstraction;
using TaskManager.Shared.DataAccess.Abstraction.Repositories;

namespace TaskManager.Shared.DataAccess.EF.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbContextContainer _dbContextContainer;
        protected DbContext Context => _dbContextContainer.CurrentDbContext;
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();
        public GenericRepository(IDbContextContainer dbContextContainer)
        {
            _dbContextContainer = dbContextContainer;
        }
        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public TEntity Find(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return DbSet.Find(predicates);
        }

        public TEntity Find(object id)
        {
            return DbSet.Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            var dbEntity = DbSet.Add(entity);
            return dbEntity.Entity;
        }

        public IQueryable<TEntity> Search(params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = DbSet;
            query = predicates.Aggregate(query, (q, exp) => q.Where(exp));
            return query;
        }

        public IQueryable<TEntity> SearchWithIncludes(Expression<Func<TEntity, object>>[] includeExpression, params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = Search(predicates);
            query = includeExpression.Aggregate(query, (q, exp) => q.Include(exp));
            return query;
        }

        public TEntity Update(TEntity entity)
        {
            var dbEntity = DbSet.Update(entity);
            return dbEntity.Entity;
        }
    }
}
