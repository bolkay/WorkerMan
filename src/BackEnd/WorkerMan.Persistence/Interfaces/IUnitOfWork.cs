using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Entities.Interfaces;

namespace WorkerMan.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<TEntity> RemoveEntityAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IEntity;
        Task<TEntity> GetEntityByIdAsync<TEntity>(object id) where TEntity : class, IEntity;
        Task<TEntity> GetEntityAsync<TEntity>(object id = null, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IEntity;
        Task<int> CommitChangesAsync();
    }
}
