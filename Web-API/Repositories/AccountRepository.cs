using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingManagementDbContext context) : base(context) { }
    }
}
