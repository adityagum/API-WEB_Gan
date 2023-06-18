using Client.ViewModels;

namespace Client.Repositories.Interface;

public interface IRepository<T, X>
        where T : class
{
    Task<ResponseListVM<T>> Get();
    Task<ResponseViewModel<T>> Get(X guid);
    Task<ResponseMessageVM> Post(T entity);
    Task<ResponseMessageVM> Put(T entity);
    Task<ResponseMessageVM> Deletes(X guid);
}
