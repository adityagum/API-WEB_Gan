using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.Repositories;
using Web_API.ViewModels.Educations;
using Web_API.ViewModels.Universities;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EducationController : BaseController<Education, EducationVM>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _mapper;
    /*private readonly IMapper<University, UniversityVM> _mapper;*/
    public EducationController(IEducationRepository educationRepository,
        IMapper<Education, EducationVM> mapper) : base(educationRepository, mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
    }
}
