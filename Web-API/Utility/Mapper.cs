using System.Reflection;
using Web_API.Contracts;

namespace Web_API.Utility;

public class Mapper<TModel, TViewModel> : IMapper<TModel, TViewModel>
    where TModel : class
    where TViewModel : class
{
    /*
     * The mapper class uses dictionaries to cache the properties of the view model and model types.
     */
    private readonly Dictionary<string, PropertyInfo> _viewModelProperties;
    private readonly Dictionary<string, PropertyInfo> _modelProperties;

    /*
     * The constructor of the mapper class is used to cache the properties of the view model and model types.
     * This is done to improve performance when mapping between the two types.
     */
    public Mapper()
    {
        // Cache the properties of the view model and model types for performance
        _viewModelProperties = typeof(TViewModel).GetProperties()
                                                 .ToDictionary(p => p.Name);
        _modelProperties = typeof(TModel).GetProperties()
                                         .ToDictionary(p => p.Name);
    }

    public TViewModel Map(TModel model)
    {
        // Create a new instance of the view model using the default constructor
        var viewModel = Activator.CreateInstance<TViewModel>();

        // Loop through all the properties on the view model
        foreach (var viewModelProperty in _viewModelProperties.Values)
        {
            // Get the value of the corresponding property on the model
            if (!_modelProperties.TryGetValue(viewModelProperty.Name, out var modelProperty)) continue;
            var value = modelProperty.GetValue(model);
            if (value == null) continue;

            // If the value is not null, set it on the view model property
            viewModelProperty.SetValue(viewModel, value);
        }

        // Return the populated view model
        return viewModel;
    }

    public TModel Map(TViewModel viewModel)
    {
        // Create a new instance of the model using the default constructor
        var model = Activator.CreateInstance<TModel>();

        // Loop through all the properties on the model
        foreach (var modelProperty in _modelProperties.Values)
        {
            // Get the value of the corresponding property on the view model
            if (!_viewModelProperties.TryGetValue(modelProperty.Name, out var viewModelProperty)) continue;
            var value = viewModelProperty.GetValue(viewModel);
            if (value == null) continue;

            // If the value is not null, set it on the model property
            modelProperty.SetValue(model, value);
        }

        // Return the populated model
        return model;
    }
}
