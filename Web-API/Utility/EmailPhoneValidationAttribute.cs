using System.ComponentModel.DataAnnotations;
using Web_API.Contracts;

namespace Web_API.Utility;

public class EmailPhoneValidationAttribute : ValidationAttribute
{
    private readonly string _propertyName;

    public EmailPhoneValidationAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult($"{_propertyName} is required.");
        var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository))
                                    as IEmployeeRepository;

        var checkEmailAndPhone = employeeRepository.CheckEmailAndPhoneAndNIK(value.ToString());
        if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
        return ValidationResult.Success;
    }
}
