using Web_API.Models;
using Web_API.ViewModels.Employees;

namespace Web_API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    { 
        // Kelompok 1
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
    }
}
