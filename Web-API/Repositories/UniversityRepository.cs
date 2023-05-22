using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class UniversityRepository :GenericRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }
        public IEnumerable<University> GetByName(string name)
        {
            return _context.Set<University>().Where(u => u.Name.Contains(name));
        }

    }
}
