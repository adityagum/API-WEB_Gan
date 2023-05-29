using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Employees;
using Web_API.Others;
using Web_API.ViewModels.Accounts;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : BaseController<Employee, EmployeVM>
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Employee, EmployeVM> _mapper;
    public EmployeeController(IEmployeeRepository employeeRepository,
        IMapper<Employee, EmployeVM> mapper) : base(employeeRepository, mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    // Kel 1

    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAllMasterEmployee()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<string>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee Not Found"
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee Found",
            Data = masterEmployees
        });
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<string>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee Not Found"
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = 200,
            Status = "OK",
            Message = "Employee Found",
            Data = masterEmployees
        });
    }
    // End Kel 1
}
