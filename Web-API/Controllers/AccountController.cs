using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Repositories;
using Web_API.Utility;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Employees;
using Web_API.ViewModels.Login;
using Web_API.Others;

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

    // Kelompok 2
    [HttpPost("Register")]
    public IActionResult Register(RegisterVM registerVM)
    {
        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest(new ResponseVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration failed"
                });
            case 1:
                return BadRequest(new ResponseVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email already exists"
                });
            case 2:
                return BadRequest(new ResponseVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Phone number already exists"
                });
            case 3:
                return Ok(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Register Success",
                    Data = registerVM
                });
        }

        return Ok();
    } // End Kelompok 2

    // Kelompok 3
    [HttpPost("login")]
    public IActionResult Login(LoginVM loginVM)
    {
        var account = _accountRepository.Login(loginVM);

        if (account == null)
        {
            return NotFound(new ResponseVM<string>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found"
            });
        }

        if (account.Password != loginVM.Password)
        {
            return NotFound(new ResponseVM<string>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Password is invalid"
            });
        }

        return Ok(
            new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Successfully",
                Data = account
            });
    }
    // End Kelompok 3

    // Kelompok 6
    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
    {
        // Cek apakah email dan OTP valid
        var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
        var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
        switch (changePass)
        {
            case 0:
                return BadRequest("");
            case 1:
                return Ok("Password has been changed successfully");
            case 2:
                return BadRequest("Invalid OTP");
            case 3:
                return BadRequest("OTP has already been used");
            case 4:
                return BadRequest("OTP expired");
            case 5:
                return BadRequest("Wrong Password No Same");
            default:
                return BadRequest();
        }
        return null;

    }

    // Kelompok 5
    [HttpPost("ForgotPassword" + "{email}")]
    public IActionResult UpdateResetPass(String email)
    {

        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account Not Found"
            });
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed Update OTP"
                });
            default:
                var hasil = new AccountResetPasswordVM
                {
                    Email = email,
                    OTP = isUpdated
                };

                MailService mailService = new MailService();
                mailService.WithSubject("Kode OTP")
                           .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                                     "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                           .WithEmail(email)
                           .Send();

                return Ok(new ResponseVM<AccountResetPasswordVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Account Reset Success"
                });
        }
    } // End Kelompok 5

    [HttpGet]
    public IActionResult GetAll()
    {
        var account = _accountRepository.GetAll();
        if (account is null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Account Not Found"
            });
        }

        var resultConverted = account.Select(_accountMapper.Map).ToList();

        return Ok(new ResponseVM<List<AccountVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = resultConverted
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound(new ResponseVM<List<AccountVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid Account Not Found",
            });
        }

        var data = _accountMapper.Map(account);

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Guid Found",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _accountMapper.Map(accountVM);
        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Create Account"
            });
        }

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Create Account"
        });
    }


    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConverted = _accountMapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Update Account"
            });
        }

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Update Account"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Failed Delete Account"
            });
        }

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success, Account has been deleted"
        });
    }

}
