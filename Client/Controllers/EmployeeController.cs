using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Web_API.Repositories;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository emprepository;
    private readonly IEducationRepository educrepository;
    private readonly IUniversityRepository univrepo;

    public EmployeeController(IEmployeeRepository _emprepository, 
        IUniversityRepository _univrepo,
        IEducationRepository educrepository)
    {
        this.emprepository = _emprepository;
        this.univrepo = _univrepo;
        this.educrepository = educrepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await emprepository.Get();
        var employees = new List<Employee>();

        if (result.Data != null)
        {
            employees = result.Data.Select(e => new Employee
            {
                Guid = e.Guid,
                Nik = e.Nik,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Gender = e.Gender,
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
            }).ToList();
        }
        return View(employees);
    }

    public async Task<IActionResult> GetAllEmp()
    {
        var result = await emprepository.GetAllEmp();
        var employees = new List<GetAllEmployee>();

        if (result.Data != null)
        {
            employees = result.Data.Select(e => new GetAllEmployee
            {
                Guid = e.Guid,
                NIK = e.NIK,
                FullName = e.FullName,
                BirthDate = e.BirthDate,
                Gender = e.Gender,
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Major = e.Major,
                Degree = e.Degree,
                GPA = e.GPA,
                UniversityName = e.UniversityName,
            }).ToList();
        }
        return View(employees);
    }


    public async Task<IActionResult> Creates()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(Employee employee)
    {
        var result = await emprepository.Post(employee);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await emprepository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.Nik = result.Data.Nik;
            employee.FirstName = result.Data.FirstName;
            employee.LastName = result.Data.LastName;
            employee.BirthDate = result.Data.BirthDate;
            employee.Gender = result.Data.Gender;
            employee.HiringDate = result.Data.HiringDate;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await emprepository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {


        var result = await emprepository.Put(employee);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await emprepository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.Nik = result.Data.Nik;
            employee.FirstName = result.Data.FirstName;
            employee.LastName = result.Data.LastName;
            employee.BirthDate = result.Data.BirthDate;
            employee.Gender = result.Data.Gender;
            employee.HiringDate = result.Data.HiringDate;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
        }

        return View(employee);
    }
}


