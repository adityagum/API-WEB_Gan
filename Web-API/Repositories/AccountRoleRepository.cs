using Microsoft.Identity.Client;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class AccountRoleRepository : GenericRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }


        public IEnumerable<AccountRole> GetByAccountGuid(Guid accountId)
        {
            return _context.Set<AccountRole>().Where(e => e.AccountGuid == accountId);
        }

        public IEnumerable<AccountRole> GetByRoleGuid(Guid roleId)
        {
            return _context.Set<AccountRole>().Where(e => e.RoleGuid == roleId);
        }
    }
}
