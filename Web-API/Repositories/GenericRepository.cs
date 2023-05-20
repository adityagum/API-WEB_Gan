using Web_API.Contexts;
using Web_API.Contracts;

namespace Web_API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BookingManagementDbContext _context;

    public GenericRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    public T Create(T t)
    {
        try
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(T t)
    {
        try
        {
            _context.Set<T>().Update(t);
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
            var t = GetByGuid(guid);
            if (t == null)
            {
                return false;
            }

            _context.Set<T>().Remove(t);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T GetByGuid(Guid guid)
    {
        return _context.Set<T>().Find(guid);
    }

}
