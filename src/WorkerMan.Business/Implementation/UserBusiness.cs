using WorkerMan.Business.Interfaces;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Business.Implementation
{
    public class UserBusiness : BaseBusiness<WorkerManUser>, IUserBusiness
    {
        public UserBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
