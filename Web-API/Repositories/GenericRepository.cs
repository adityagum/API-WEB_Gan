using Web_API.Contexts;
using Web_API.Contracts;

namespace Web_API.Repositories;

/*
Repository ini berfungsi untuk melakukan interaksi dengan database, sama halnya dengan kita ingin 
menambahkan data, mengambil data, update atau delete. 
 */
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly BookingManagementDbContext _context;

    public GenericRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    public T? Create(T t)
    {
        try
        {
            typeof(T).GetProperty("CreatedDate")!
                .SetValue(t, DateTime.Now);

            typeof(T).GetProperty("ModifiedDate")!
                .SetValue(t, DateTime.Now);

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
            var guid = (Guid) typeof(T).GetProperty("Guid")!
                                       .GetValue(t)!;
            var oldEntity = GetByGuid(guid);
            if (oldEntity == null) {
                return false;
            }

            var getCreatedDate = typeof(T).GetProperty("CreatedDate")!
                                          .GetValue(oldEntity)!;

            typeof(T).GetProperty("CreatedDate")!
                .SetValue(t, getCreatedDate);

            typeof(T).GetProperty("ModifiedDate")!
                .SetValue(t, DateTime.Now);

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
        var entity = _context.Set<T>().Find(guid);
        _context.ChangeTracker.Clear();
        return entity;
    }

    public T GetByName(T t)
    {
        return _context.Set<T>().Find(t);
    }

}
