using Web_API.Models;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Login;

namespace Web_API.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        AccountEmpVM Login(LoginVM loginVM);
    }
}
