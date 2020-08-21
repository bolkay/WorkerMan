using System.Linq;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Entities.Models;

namespace WorkerMan.Persistence.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<WorkerCompany>
    {
        Task<IQueryable<WorkerCompany>> GetAllCompanies();
    }
}
