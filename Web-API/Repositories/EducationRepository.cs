using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class EducationRepository : IEducationRepository
    {

        private readonly BookingManagementDbContext _context;
        public EducationRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Education Create(Education education)
        {
            try
            {
                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return new Education();
            }
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Set<Education>().Update(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var education = GetByGuid(guid);
                if (education == null)
                {
                    return false;
                }

                _context.Set<Education>().Remove(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList();
        }

        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid);
        }

        
    }
}
