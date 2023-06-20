using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string jwToken = HttpContext.Session.GetString("JWToken") ?? "JWT is null";
        ViewData["JWToken"] = jwToken;
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/Unauthorized")]
    public IActionResult Unauthorized()
    {
        return View("401");
    }

    [AllowAnonymous]
    [HttpGet("/Forbidden")]
    public IActionResult Forbidden()
    {
        return View("403");
    }

    [AllowAnonymous]
    [HttpGet("/NotFound")]
    public IActionResult NotFound()
    {
        return View("404");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}