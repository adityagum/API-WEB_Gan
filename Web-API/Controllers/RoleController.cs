using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
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
            return NotFound();
        }

        var data = role.Select(_roleMapper.Map).ToList();

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound();
        }

        var data = _roleMapper.Map(role);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(RoleVM roleVM)
    {
        var empconverted = _roleMapper.Map(roleVM);
        var result = _roleRepository.Create(empconverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(RoleVM roleVM)
    {
        var empconverted = _roleMapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(empconverted);
        if (!isUpdated)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
