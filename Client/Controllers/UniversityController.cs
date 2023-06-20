using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;


public class UniversityController : Controller
{
    private readonly IUniversityRepository repository;

    public UniversityController(IUniversityRepository repository)
    {
        this.repository = repository;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var universities = new List<University>();

        if (result.Data != null)
        {
            universities = result.Data.Select(e => new University
            {
                guid = e.guid,
                Code = e.Code,
                Name = e.Name,

            }).ToList();
        }
        return View(universities);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Creates()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Creates(University university)
    {
        var result = await repository.Post(university);
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

    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await repository.Get(guid);
        var university = new University();
        if (result.Data?.guid is null)
        {
            return View(university);
        }
        else
        {
            university.guid = result.Data.guid;
            university.Code = result.Data.Code;
            university.Name = result.Data.Name;
        }
        return View(university);
    }

    [HttpPost]
	[Authorize(Roles = "Admin, Manager")]
	public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await repository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
	[Authorize(Roles = "Admin, Manager")]
	public async Task<IActionResult> Edit(University university)
    {


        var result = await repository.Put(university);
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
	[Authorize(Roles = "Admin, Manager")]
	public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await repository.Get(guid);
        var university = new University();
        if (result.Data?.guid is null)
        {
            return View(university);
        }
        else
        {
            university.guid = result.Data.guid;
            university.Code = result.Data.Code;
            university.Name = result.Data.Name;
        }

        return View(university);
    }

}