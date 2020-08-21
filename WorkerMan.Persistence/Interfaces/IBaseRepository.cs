using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkerMan.Persistence.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddOneAsync(TEntity entity);
        Task<IQueryable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities);
        Task<TEntity> RemoveAsync(TEntity entity);
        Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindOne(object id = null, Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IQueryable<TEntity>> UpdateManyAsync(IEnumerable<TEntity> entities);
    }
}
