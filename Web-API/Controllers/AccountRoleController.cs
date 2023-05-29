using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.ViewModels.AccountRoles;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : BaseController<AccountRole, AccountRolesVM>
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<AccountRole, AccountRolesVM> _mapper;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository,
        IMapper<AccountRole, AccountRolesVM> mapper) : base(accountRoleRepository, mapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
    }
}
