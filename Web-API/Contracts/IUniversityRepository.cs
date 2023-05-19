using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IUniversityRepository
    {
        University Create(University university);
        bool Update(University university);
        bool Delete(Guid guid);
        IEnumerable<University> GetAll();
        University? GetByGuid(Guid guid);
    }
}
