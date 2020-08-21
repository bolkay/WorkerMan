using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Entities.Interfaces;

namespace WorkerMan.Business.Interfaces
{
    public interface IBaseBusiness<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetEntityAsync(object id = null, Expression<Func<TEntity, bool>> predicate = null);
        Task<IQueryable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> AddEntityAsync(TEntity entity);

        IQueryable<TEntity> AddEntitiesAsync(IEnumerable<TEntity> entities);
        Task<TEntity> RemoveEntityAsync(TEntity entity);
        IQueryable<TEntity> RemoveEntitiesAsync(IEnumerable<TEntity> entities);
        TEntity UpdateEntityAsync(TEntity entity);
    }
}
