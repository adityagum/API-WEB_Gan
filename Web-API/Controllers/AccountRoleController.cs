using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
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
            return NotFound(new ResponseVM<AccountRolesVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.OK.ToString(),
                Message = " Data was not found"
            });
        }

        var result = accountrole.Select(_armapper.Map).ToList();

        return Ok(new ResponseVM<List<AccountRolesVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data accountrole successfully found",
            Data = result
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountrole = _accountRoleRepository.GetByGuid(guid);
        if (accountrole is null)
        {
            return NotFound(new ResponseVM<AccountRolesVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid not found"
            });
        }

        var data = _armapper.Map(accountrole);

        return Ok(new ResponseVM<AccountRolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Guid",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(AccountRolesVM accountRolesVM)
    {
        var arConverted = _armapper.Map(accountRolesVM);
        var result = _accountRoleRepository.Create(arConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountRolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create AccountRole Failed"
            });
        }

        return Ok(new ResponseVM<AccountRolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create Account Success"
        });
    }


    [HttpPut]
    public IActionResult Update(AccountRolesVM accountRolesVM)
    {
        var arConverted = _armapper.Map(accountRolesVM);
        var isUpdated = _accountRoleRepository.Update(arConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountRolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update AccountRole Failed"
            });
        }

        return Ok(new ResponseVM<AccountRolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create AccountRole Success"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountRolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete AccountRole Failed"
            });
        }

        return Ok(new ResponseVM<AccountRolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete AccountRole Success"
        });
    }
}
