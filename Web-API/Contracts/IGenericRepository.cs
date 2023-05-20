namespace Web_API.Contracts;

public interface IGenericRepository <T> where T : class
{
    T Create(T t);
    bool Update(T t);
    bool Delete(Guid guid);
    IEnumerable<T> GetAll();
    T GetByGuid(Guid guid);
}
