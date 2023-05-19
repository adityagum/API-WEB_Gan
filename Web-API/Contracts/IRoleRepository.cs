using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        bool Update(Role role);
        bool Delete(Guid guid);
        IEnumerable<Role> GetAll();
        Role? GetByGuid(Guid guid);
    }
}
