using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.CrossCutting.Entities.Interfaces;
using WorkerMan.CrossCutting.Entities.Models;
using WorkerMan.Persistence.Interfaces;
using WorkerMan.Persistence.Lookup;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq;
using System.Linq.Expressions;

namespace WorkerMan.Persistence.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkerManContext workerManContext;
        private readonly RepositoryMapper<IEntity> repositoryMapper;

        public IUserRepository UserRepository { get; }
        public ICompanyRepository CompanyRepository { get; }

        public UnitOfWork(WorkerManContext workerManContext, RepositoryMapper<IEntity> repositoryMapper
            , IUserRepository userRepository, ICompanyRepository companyRepository)
        {

            this.workerManContext = workerManContext;
            this.repositoryMapper = repositoryMapper;
            UserRepository = userRepository;
            CompanyRepository = companyRepository;

            //Fill the repository.
            this.repositoryMapper
                .AddEntityRepositoryMap(new WorkerManUser(), UserRepository)
                .AddEntityRepositoryMap(new WorkerCompany(), CompanyRepository);
        }
        public async Task<int> CommitChangesAsync()
        {
            return await workerManContext.SaveChangesAsync();
        }

        public void Dispose()
        {

        }

        public async Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            TEntity result = null;
            IBaseRepository<TEntity> repository = repositoryMapper.FindRepository<TEntity>();

            if (null != repository)
            {
                result = await repository.AddOneAsync(entity);
            }

            return result;
        }

        public async Task<TEntity> RemoveEntityAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            TEntity result = null;
            IBaseRepository<TEntity> repository = repositoryMapper.FindRepository<TEntity>();

            if (null != repository)
            {
                result = await repository.RemoveAsync(entity);
            }

            return result;
        }

        public async Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> result = null;

            IBaseRepository<TEntity> repository = repositoryMapper.FindRepository<TEntity>();

            if (repository != null)
            {
                result = await repository.FindManyAsync(predicate);
            }

            return result;
        }

        public async Task<TEntity> GetEntityByIdAsync<TEntity>(object id) where TEntity : class, IEntity
        {
            TEntity result = null;

            if (id != null)
            {
                IBaseRepository<TEntity> repository = repositoryMapper.FindRepository<TEntity>();

                if (null != repository)
                {
                    result = await repository.FindOne(id);
                }
            }

            return result;
        }

        public async Task<TEntity> GetEntityAsync<TEntity>(object id = null, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IEntity
        {
            TEntity result = null;

            IBaseRepository<TEntity> repository = repositoryMapper.FindRepository<TEntity>();

            if (repository != null)
            {
                result = await repository.FindOne(id, predicate);
            }
            return result;
        }
    }
}
