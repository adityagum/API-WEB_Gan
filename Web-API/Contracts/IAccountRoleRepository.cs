using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IAccountRoleRepository : IGenericRepository<AccountRole>
    {
        IEnumerable<AccountRole> GetByAccountGuid(Guid accountId);
        IEnumerable<AccountRole> GetByRoleGuid(Guid roleId);
    }
}
