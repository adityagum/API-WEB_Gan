using Web_API.Models;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Login;

namespace Web_API.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        LoginVM Login(LoginVM loginVM);

        // Kelompok 2
        int Register(RegisterVM registerVM);

        // Kelompok 5
        int UpdateOTP(Guid? employeeId);

        // Kelompok 6
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

        IEnumerable<string> GetRoles(Guid guid);

    }
}
