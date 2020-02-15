using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TaskManager.Shared.DataAccess.Abstraction.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> Search(params Expression<Func<TEntity, bool>>[] predicates);
        IQueryable<TEntity> SearchWithIncludes(Expression<Func<TEntity, object>>[] includeExpression, 
            params Expression<Func<TEntity, bool>>[] predicates);

        TEntity Find(params Expression<Func<TEntity, bool>>[] predicates);
        TEntity Find(object id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
