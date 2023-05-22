namespace Web_API.Contracts
{
    public interface IMapper<TModel, TViewModel>
    {
        TViewModel Map(TModel model);
        TModel Map(TViewModel viewModel);
    }
}
