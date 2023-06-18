using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        public Task<ResponseListVM<GetAllEmployee>> GetAllEmp();
    }
}
