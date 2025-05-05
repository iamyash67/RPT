using Microsoft.AspNetCore.Mvc;
using RPT.Models;
using RPT.Services;

namespace RPT.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService) => _profileService = profileService;

    [HttpGet("{id}")]
    /*public IActionResult GetProfile(int id) => 
        _profileService.GetProfileById(id) is Profile profile ? Ok(profile) : NotFound("Profile not found");*/
    public IActionResult GetProfile(int id)
    {
    var profile = _profileService.GetProfileById(id);
    
    if (profile != null)
    {
        return Ok(profile);
    }
    else
    {
        return NotFound("Profile not found");
    }
    }


    /*[HttpPost]
    public IActionResult CreateProfile([FromBody] Profile newProfile)
    {
        _profileService.CreateProfile(newProfile);
        return Ok("Profile created successfully");
    }*/

    [HttpPut("{id}")]
    /*public IActionResult UpdateProfile(int id, [FromBody] Profile updatedProfile) =>
        id != updatedProfile.ProfileId 
        ? BadRequest("Profile ID cannot be changed") 
        : _profileService.UpdateProfile(updatedProfile) ? Ok("Profile updated successfully") : NotFound("Profile not found");*/
    public IActionResult UpdateProfile(int id, [FromBody] Profile updatedProfile)
    {
    if (id != updatedProfile.ProfileId)
    {
        return BadRequest("Profile ID cannot be changed");
    }

    if (_profileService.UpdateProfile(updatedProfile))
    {
        return Ok("Profile updated successfully");
    }
    else
    {
        return NotFound("Profile not found");
    }
    }


}