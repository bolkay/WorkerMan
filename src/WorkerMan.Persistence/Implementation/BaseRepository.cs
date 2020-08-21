using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Persistence.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public BaseRepository(WorkerManContext workerManContext)
        {
            WorkerManContext = workerManContext;
        }

        public WorkerManContext WorkerManContext { get; }

        public async Task<IQueryable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                WorkerManContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await WorkerManContext.Set<TEntity>().AddRangeAsync(entities);

            return entities.AsQueryable();
        }

        public async Task<TEntity> AddOneAsync(TEntity entity)
        {
            WorkerManContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await WorkerManContext.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public async Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(WorkerManContext.Set<TEntity>()
                .Where(predicate)
                .AsQueryable());
        }

        public async Task<TEntity> FindOne(object id = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            TEntity result = null;

            if (id == null)
                result = WorkerManContext.Set<TEntity>().FirstOrDefault(predicate);
            else
                result = WorkerManContext.Set<TEntity>().Find(id);

            return await Task.FromResult(result);
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            WorkerManContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await Task.Run(() =>
            {
                WorkerManContext.Set<TEntity>()
                                .Remove(entity);
            });

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            WorkerManContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await Task.Run(() => { WorkerManContext.Set<TEntity>().Update(entity); });

            return entity;
        }

        public async Task<IQueryable<TEntity>> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                WorkerManContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            await Task.Run(() => { WorkerManContext.Set<TEntity>().UpdateRange(entities); });

            return entities.AsQueryable();
        }
    }
}