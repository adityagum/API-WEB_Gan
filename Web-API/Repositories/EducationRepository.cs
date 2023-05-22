using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories;

public class EducationRepository : GenericRepository<Education>, IEducationRepository
{
    public EducationRepository(BookingManagementDbContext context) : base(context) { }

    public IEnumerable<Education> GetByUniversityId(Guid universityId)
    {
        return _context.Set<Education>().Where(e => e.UniversityGuid == universityId);
    }
}
