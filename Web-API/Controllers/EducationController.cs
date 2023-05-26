using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
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
        var education = _educationRepository.GetAll();
        if (!education.Any())
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data was not found"
            });
        }
        var resultConverted = education.Select(_educationVMMapper.Map).ToList();

        return Ok(new ResponseVM<List<EducationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Education",
            Data = resultConverted
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _educationRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid not found"
            });
        }

        var data = _educationVMMapper.Map(room);

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found the data education by Guid",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var educationConverted = _educationVMMapper.Map(educationVM);
        var result = _educationRepository.Create(educationConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest
                    ,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Something wrong, please check your input. Create education failed.."
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Create Education"
        });
    }


    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationConverted = _educationVMMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest
                    ,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed to update Education"
            });
        }
        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Update Education"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest
                    ,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Guid was not found, failed to delete education"
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Delete Education"
        });
    }

}
