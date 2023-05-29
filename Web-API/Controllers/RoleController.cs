using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.ViewModels.Roles;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]

public class RoleController : BaseController<Role, RoleVM>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RoleVM> _mapper;

    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper) : base(roleRepository, mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
}
