using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IEducationRepository : IGenericRepository<Education>
    {
        IEnumerable<Education> GetByUniversityId(Guid universityId);
    }
}
