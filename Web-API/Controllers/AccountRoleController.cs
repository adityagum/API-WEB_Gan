using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountRoleRepository;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountRole = _accountRoleRepository.GetAll();
        if (accountRole is null)
        {
            return NotFound();
        }

        return Ok(accountRole);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return NotFound();
        }

        return Ok(accountRole);
    }

    [HttpPost]
    public IActionResult Create(AccountRole accountRole)
    {
        var result = _accountRoleRepository.Create(accountRole);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(AccountRole accountRole)
    {
        var isUpdated = _accountRoleRepository.Update(accountRole);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
