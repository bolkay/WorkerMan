using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkerMan.Business.Interfaces;
using WorkerMan.CrossCutting.Entities.Interfaces;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Business.Implementation
{
    public class BaseBusiness<TEntity> : IBaseBusiness<TEntity> where TEntity : class, IEntity
    {
        public BaseBusiness(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public IQueryable<TEntity> AddEntitiesAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            TEntity result = null;

            if (entity != null)
            {
                result = await UnitOfWork.AddEntityAsync<TEntity>(entity);

                if (result != null)
                {
                    await UnitOfWork.CommitChangesAsync();
                }
            }
            return result;
        }

        public async Task<IQueryable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> result = null;

            if (UnitOfWork != null)
            {
                result = await UnitOfWork.GetEntitiesAsync(predicate);
            }

            return result;
        }

        public async Task<TEntity> GetEntityAsync(object id = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            TEntity result = null;

            if (UnitOfWork != null)
            {
                result = await UnitOfWork.GetEntityAsync(id, predicate);
            }

            return result;
        }

        public IQueryable<TEntity> RemoveEntitiesAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> RemoveEntityAsync(TEntity entity)
        {
            TEntity result = null;

            if (entity != null)
            {
                result = await UnitOfWork.RemoveEntityAsync(entity);

                if (result != null)
                {
                    await UnitOfWork.CommitChangesAsync();
                }
            }

            return result;
        }

        public TEntity UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
