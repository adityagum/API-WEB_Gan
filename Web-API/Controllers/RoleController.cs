using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.ViewModels.Roles;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]

public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RoleVM> _roleMapper;

    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> roleMapper)
    {
        _roleRepository = roleRepository;
        _roleMapper = roleMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var role = _roleRepository.GetAll();
        if (role is null)
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        var data = role.Select(_roleMapper.Map).ToList();

        return Ok(new ResponseVM<List<RoleVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Role",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        var data = _roleMapper.Map(role);

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Guid Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(RoleVM roleVM)
    {
        var empconverted = _roleMapper.Map(roleVM);
        var result = _roleRepository.Create(empconverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create Role Failed"
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create Role Success"
        });
    }


    [HttpPut]
    public IActionResult Update(RoleVM roleVM)
    {
        var empconverted = _roleMapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(empconverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update Role Failed"
            });
        }
        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Role Success"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete Role Failed"
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete Role Success"
        });
    }
}
