using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Repositories;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Universities;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EducationController : ControllerBase
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _educationVMMapper;
    private readonly IMapper<University, UniversityVM> _mapper;
    public EducationController(IEducationRepository educationRepository, 
        IMapper<University, UniversityVM> mapper, 
        IMapper<Education, EducationVM> educationMapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationVMMapper = educationMapper;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        /*var room = _educationRepository.GetAll();
        if (room is null)
        {
            return NotFound();
        }

        var resultConverted = room.Select(EducationVM.ToVM);
        return Ok(resultConverted);
        return Ok(room);*/

        var education = _educationRepository.GetAll();
        if (!education.Any())
        {
            return NotFound();
        }
        var resultConverted = education.Select(_educationVMMapper.Map).ToList();

        return Ok(education);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _educationRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        var data = _educationVMMapper.Map(room);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var educationConverted = _educationVMMapper.Map(educationVM);
        var result = _educationRepository.Create(educationConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationConverted = _educationVMMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConverted);
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
