using Microsoft.AspNetCore.Mvc;
using RPT.Models;
using RPT.Services;

namespace RPT.Controllers;

[ApiController]
[Route("api/signup")]
public class SignupController : ControllerBase
{
    private readonly ISignupService _signupService;

    public SignupController(ISignupService signupService) => _signupService = signupService;

    [HttpPost]
    /*public IActionResult Register([FromBody] Profile newProfile) =>
        _signupService.Signup(newProfile) ? Ok("Signup successful!") : Conflict("Username already exists");*/
    public IActionResult Register([FromBody] Profile newProfile)
    {
    if (_signupService.Signup(newProfile))
    {
        return Ok("Signup successful!");
    }
    return Conflict("Username already exists");
    }

}
