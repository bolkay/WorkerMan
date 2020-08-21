using System;
using System.Linq;
using System.Linq.Expressions;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Persistence.Implementation
{
    public class UserRepository : BaseRepository<WorkerManUser>, IUserRepository
    {
        public UserRepository(WorkerManContext workerManContext) : base(workerManContext)
        {

        }
        public IQueryable<WorkerManUser> GetAllWorkerManUsers()
        {
            return WorkerManContext.Set<WorkerManUser>().AsQueryable();
        }

        public IQueryable<WorkerManUser> GetUsers(Expression<Func<WorkerManUser, bool>> predicate)
        {
            return WorkerManContext.Set<WorkerManUser>()
                .Where(predicate);
        }
    }
}
