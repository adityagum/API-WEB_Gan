using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.AccountRoles;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<AccountRole, AccountRolesVM> _armapper;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository, 
        IMapper<AccountRole, AccountRolesVM> armapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _armapper = armapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountrole = _accountRoleRepository.GetAll();
        if (accountrole is null)
        {
            return NotFound();
        }

        var result = accountrole.Select(_armapper.Map).ToList();

        return Ok(accountrole);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountrole = _accountRoleRepository.GetByGuid(guid);
        if (accountrole is null)
        {
            return NotFound();
        }

        var data = _armapper.Map(accountrole);

        return Ok(accountrole);
    }

    [HttpPost]
    public IActionResult Create(AccountRolesVM accountRolesVM)
    {
        var arConverted = _armapper.Map(accountRolesVM);
        var result = _accountRoleRepository.Create(arConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(AccountRolesVM accountRolesVM)
    {
        var arConverted = _armapper.Map(accountRolesVM);
        var isUpdated = _accountRoleRepository.Update(arConverted);
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
