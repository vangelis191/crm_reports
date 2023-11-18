using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.services;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AuthService _auth;
    private readonly EmployeeService _service;

    public HomeController(ILogger<HomeController> logger,AuthService auth, EmployeeService service)
    {
        _logger = logger;
        _auth = auth;
        _service = service;

    }

    public IActionResult Index()
    {

        return View();
    }
    
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        
        return View();
    }

    [HttpGet("/GetEmployee")]
    public void GetEmployee()
    {
        _service.GetAllEmployees();
    }
    
    [HttpPost("/login")]
    public IActionResult Login(string username, string password)
    {
        if (_auth.ValidateUserCredentials(username, password))
        {
            var role = _auth.GetUserRole(username);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                // Add roles as claims
                new Claim(ClaimTypes.Role, string.Join(",", role))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Additional authentication properties if needed
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Dashboard");
        }
        else
        {
   
            return RedirectToAction("Login");
        }
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