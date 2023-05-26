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
public class EmployeeController : ControllerBase
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IMapper<Employee, EmployeVM> _empmapper;
    public EmployeeController(IEmployeeRepository employeeRepository, 
        IMapper<Employee, EmployeVM> empmapper,
        IEducationRepository educationRepository,
        IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _empmapper = empmapper;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Employee Not Found"
            });
        }

        var empresult = employees.Select(_empmapper.Map).ToList();

        return Ok(new ResponseVM<IEnumerable<AccountVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = (IEnumerable<AccountVM>)empresult
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound(new ResponseVM<List<EmployeVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid by Employee Not Found",
            });
        }

        var empresult = _empmapper.Map(employee);

        return Ok(new ResponseVM<EmployeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found the Guid",
            Data = empresult
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeVM employeVM)
    {
        var empconverted = _empmapper.Map(employeVM);
        var result = _employeeRepository.Create(empconverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EmployeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed to create Employee, please check your input type"
            });
        }

        return Ok(new ResponseVM<EmployeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Create Employee"
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeVM employeVM)
    {
        var empconverted = _empmapper.Map(employeVM);
        var isUpdated = _employeeRepository.Update(empconverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EmployeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Update Employee"
            });
        }

        return Ok(new ResponseVM<EmployeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Update Employee"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EmployeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Guid was not found, failed to delete Employee"
            });
        }

        return Ok(new ResponseVM<EmployeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Delete Employee"
        });
    }
}
