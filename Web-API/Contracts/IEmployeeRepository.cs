using Web_API.Models;
using Web_API.ViewModels.Employees;

namespace Web_API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    { 
        // Kelompok 1
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        // Kelompok 2
        int CreateWithValidate(Employee employee);

        // Kelompok 5
        Guid? FindGuidByEmail(string email);
    }
}
