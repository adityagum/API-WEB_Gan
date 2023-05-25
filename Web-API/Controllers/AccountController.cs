using Microsoft.AspNetCore.Mvc;
using System;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Repositories;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Login;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Account, AccountVM> _accountMapper;

    public AccountController(IAccountRepository accountRepository, 
        IMapper<Account, AccountVM> accountMapper,
        IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _accountMapper = accountMapper;
        _employeeRepository = employeeRepository;
    }

    [HttpPost("login")]

    public IActionResult Login(LoginVM loginVM)
    {
        var account = _accountRepository.Login(loginVM);

        if (account == null)
        {
            return NotFound("Account not found");
        }

        if (account.Password != loginVM.Password)
        {
            return BadRequest("Password is invalid");
        }

        return Ok();

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var account = _accountRepository.GetAll();
        if (account is null)
        {
            return NotFound();
        }
        var resultConverted = account.Select(_accountMapper.Map).ToList();

        return Ok(account);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound();
        }

        var data = _accountMapper.Map(account);

        return Ok(account);
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _accountMapper.Map(accountVM);
        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConverted = _accountMapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}
