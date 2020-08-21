using System;
using System.Linq;
using System.Linq.Expressions;
using WorkerMan.CrossCutting.Entities.Identity;

namespace WorkerMan.Persistence.Interfaces
{
    public interface IUserRepository : IBaseRepository<WorkerManUser>
    {
        IQueryable<WorkerManUser> GetAllWorkerManUsers();
        IQueryable<WorkerManUser> GetUsers(Expression<Func<WorkerManUser, bool>> predicate);
    }
}
