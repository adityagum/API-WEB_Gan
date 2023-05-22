using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Utility;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Universities;


namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationMapper;
    public UniversityController(IUniversityRepository universityRepository, 
        IEducationRepository educationRepository, 
        IMapper<University, UniversityVM> mapper, 
        IMapper<Education, EducationVM> educationMapper)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationMapper = educationMapper;
    }

    /*[HttpGet("WithEducation")]
    public IActionResult GetAllWithEducation()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound();
        }

        var results = new List<UniversityEducationVM>();
        foreach (var university in universities)
        {
            var education = _educationRepository.GetByUniversityId(university.Guid);
            var educationMapped = education.Select(_mapper.Map(EducationVM));

            var result = new UniversityEducationVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name,
                Educations = educationMapped
            };

            results.Add(result);
        }

        return Ok(results);
    }*/

    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound();
        }

        var data = universities.Select(_mapper.Map).ToList();

        return Ok(data);

    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound();
        }

        var data = _mapper.Map(university);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        /*var universityConverted = UniversityVM.ToModel(universityVM);
        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);*/

        var universityConverted = _mapper.Map(universityVM);
        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(UniversityVM universityVM)
    {
        var universityConverted = _mapper.Map(universityVM);
        var isUpdated = _universityRepository.Update(universityConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        /*var data = _mapper.Map();*/

        return Ok();
    }

    /*[HttpGet("search/{name}")]
    public IActionResult SearchByName(string name)
    {
        var universities = _universityRepository.GetByName(name);
        return Ok(universities);
    }*/
}
