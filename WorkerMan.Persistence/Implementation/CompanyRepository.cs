using System.Linq;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.CrossCutting.Entities.Models;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Persistence.Implementation
{
    public class CompanyRepository : BaseRepository<WorkerCompany>, ICompanyRepository
    {
        public CompanyRepository(WorkerManContext workerManContext) : base(workerManContext)
        {

        }
        public async Task<IQueryable<WorkerCompany>> GetAllCompanies()
        {
            return
                await Task.FromResult(
                WorkerManContext.Set<WorkerCompany>()
                .AsQueryable());
        }
    }
}
