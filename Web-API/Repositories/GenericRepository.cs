using Web_API.Contexts;
using Web_API.Contracts;

namespace Web_API.Repositories;

/*
Repository ini berfungsi untuk melakukan interaksi dengan database, sama halnya dengan kita ingin 
menambahkan data, mengambil data, update atau delete. 
 */
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly BookingManagementDbContext _context;

    public GenericRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    public TEntity? Create(TEntity tentity)
    {
        try
        {
            typeof(TEntity).GetProperty("CreatedDate")!
                .SetValue(tentity, DateTime.Now);

            typeof(TEntity).GetProperty("ModifiedDate")!
                .SetValue(tentity, DateTime.Now);

            _context.Set<TEntity>().Add(tentity);
            _context.SaveChanges();
            return tentity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEntity tentity)
    {
        try
        {
            var guid = (Guid) typeof(TEntity).GetProperty("Guid")!
                                       .GetValue(tentity)!;
            var oldEntity = GetByGuid(guid);
            if (oldEntity == null) {
                return false;
            }

            var getCreatedDate = typeof(TEntity).GetProperty("CreatedDate")!
                                          .GetValue(oldEntity)!;

            typeof(TEntity).GetProperty("CreatedDate")!
                .SetValue(tentity, getCreatedDate);

            typeof(TEntity).GetProperty("ModifiedDate")!
                .SetValue(tentity, DateTime.Now);

            _context.Set<TEntity>().Update(tentity);
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

            _context.Set<TEntity>().Remove(t);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity GetByGuid(Guid guid)
    {
        var entity = _context.Set<TEntity>().Find(guid);
        _context.ChangeTracker.Clear();
        return entity;
    }

    public TEntity GetByName(TEntity tentity)
    {
        return _context.Set<TEntity>().Find(tentity);
    }

}
