using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Employees;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Employee, EmployeVM> _empmapper;
    public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeVM> empmapper)
    {
        _employeeRepository = employeeRepository;
        _empmapper = empmapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound();
        }

        var empresult = employees.Select(_empmapper.Map).ToList();

        return Ok(empresult);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound();
        }

        var empresult = _empmapper.Map(employee);

        return Ok(empresult);
    }

    [HttpPost]
    public IActionResult Create(EmployeVM employeVM)
    {
        var empconverted = _empmapper.Map(employeVM);
        var result = _employeeRepository.Create(empconverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EmployeVM employeVM)
    {
        var empconverted = _empmapper.Map(employeVM);
        var isUpdated = _employeeRepository.Update(empconverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
