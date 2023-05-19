using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories;

public class UniversityRepository : IUniversityRepository
{
    private readonly BookingManagementDbContext _context;
    public UniversityRepository(BookingManagementDbContext context)
    {
        _context = context;
    }
    public University Create(University university)
    {
        try
        {
            _context.Set<University>().Add(university);
            _context.SaveChanges();
            return university;
        }
        catch
        {
            return new University();
        }
    }

    public bool Update(University university)
    {
        try
        {
            _context.Set<University>().Update(university);
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
            var university = GetByGuid(guid);
            if (university == null)
            {
                return false;
            }

            _context.Set<University>().Remove(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<University> GetAll()
    {
        return _context.Set<University>().ToList();
    }

    public University? GetByGuid(Guid guid)
    {
        return _context.Set<University>().Find(guid);
    }

}

    
