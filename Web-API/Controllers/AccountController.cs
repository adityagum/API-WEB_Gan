using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Login;
using Web_API.Others;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController<Account, AccountVM>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IEmailService _emailService;

    public AccountController(IAccountRepository accountRepository,
        IMapper<Account, AccountVM> mapper,
        IEmployeeRepository employeeRepository,
        IEmailService emailService) : base(accountRepository, mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
        _emailService = emailService;
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
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "ChangePassword Failed"
                });
            case 1:
                return Ok(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "ChangePassword Success"
                });
            case 2:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "invalidOTP "
                });
            case 3:

                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP has already been used"
                });
            case 4:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP expired"
                });
            case 5:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Wrong Password No Same"
                });
            default:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Error"
                });
        }
    }

    // Kelompok 5
    [HttpPost("ForgotPassword" + "{email}")]
    public IActionResult UpdateResetPass(String email)
    {
        var response = new ResponseVM<AccountResetPasswordVM>();
        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest();
            default:
                var hasil = new AccountResetPasswordVM
                {
                    Email = email,
                    OTP = isUpdated
                };

                /* MailService mailService = new MailService();
            mailService.WithSubject("Kode OTP")
                       .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                                 "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                       .WithEmail(email)
                       .Send();*/

                _emailService.SetEmail(email)
                    .SetSubject("Forgot Passowrd")
                    .SetHtmlMessage($"Your OTP is {isUpdated}")
                    .SendEmailAsync();

                return Ok(new ResponseVM<List<AccountVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "OTP has been sent to your email"
                });

        }
    }
    // End Kelompok 5
}
