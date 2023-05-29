using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.Utility;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Universities;


namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UniversityController : BaseController<University, UniversityVM>
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    public UniversityController(IUniversityRepository universityRepository,
        IMapper<University, UniversityVM> mapper) : base(universityRepository, mapper)
    {
        _universityRepository = universityRepository;
        _mapper = mapper;
    }
}
