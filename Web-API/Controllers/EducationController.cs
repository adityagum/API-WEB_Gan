using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Repositories;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EducationController : ControllerBase
{
    private readonly IGenericRepository<Education> _educationRepository;

    public EducationController(IGenericRepository<Education> educationRepository)
    {
        _educationRepository = educationRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _educationRepository.GetAll();
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _educationRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpPost]
    public IActionResult Create(Education education)
    {
        var result = _educationRepository.Create(education);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(Education education)
    {
        var isUpdated = _educationRepository.Update(education);
        if (!isUpdated)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}
