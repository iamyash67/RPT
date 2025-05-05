using Microsoft.AspNetCore.Mvc;
using RPT.Models;
using RPT.Services;

namespace RPT.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService) => _loginService = loginService;

    [HttpPost]
    /*public IActionResult ValidateLogin([FromBody] Login login) =>
        _loginService.Login(login.UserName, login.Password) is Profile profile 
        ? Ok(profile) 
        : Unauthorized("Invalid username or password");*/
    public IActionResult ValidateLogin([FromBody] Login login)
    {
    var profile = _loginService.Login(login.UserName, login.Password);
    
    if (profile != null)
    {
        return Ok(profile);
    }
    else
    {
        return Unauthorized("Invalid username or password");
    }
    }

}
