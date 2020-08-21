using WorkerMan.Business.Interfaces;
using WorkerMan.CrossCutting.Entities.Models;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Business.Implementation
{
    public class CompanyBusiness : BaseBusiness<WorkerCompany>, ICompanyBusiness
    {
        public CompanyBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
