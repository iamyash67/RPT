using Microsoft.AspNetCore.Mvc;
using RPT.Models;
using RPT.Services;

namespace RPT.Controllers;

[ApiController]
[Route("api/goal")]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalController(IGoalService goalService) => _goalService = goalService;

    [HttpGet("{id}")]
    /*public IActionResult GetGoal(int id) =>
        _goalService.GetGoalById(id) is Goal goal ? Ok(goal) : NotFound("Goal not found");*/
    public IActionResult GetGoal(int id)
    {
    var goal = _goalService.GetGoalById(id);
    
    if (goal != null)
    {
        return Ok(goal);
    }
    else
    {
        return NotFound("Goal not found");
    }
    }


    [HttpPost]
    /*public IActionResult CreateGoal([FromBody] Goal newGoal) =>
        newGoal.GoalId != newGoal.ProfileId 
        ? BadRequest("Goal ID must match Profile ID") 
        : _goalService.CreateGoal(newGoal)? Ok("Goal created successfully"): NotFound("Creation failed");*/
    public IActionResult CreateGoal([FromBody] Goal newGoal)
    {
    /*if (newGoal.GoalId != newGoal.ProfileId)
    {
        return BadRequest("Goal ID must match Profile ID");
    }*/
    if (_goalService.GoalExists(newGoal.ProfileId)) 
    {
        return Conflict("A goal already exists for this profile.");
    }
    if (_goalService.CreateGoal(newGoal))
    {
        return Ok("Goal created successfully");
    }
    else
    {
        return NotFound("Creation failed");
    }
    }


    [HttpPut("{id}")]
    /*public IActionResult UpdateGoal(int id, [FromBody] Goal updatedGoal) =>
        id != updatedGoal.GoalId 
        ? BadRequest("Goal ID cannot be changed") 
        : _goalService.UpdateGoal(updatedGoal) ? Ok("Goal updated successfully") : NotFound("Goal not found");*/
    public IActionResult UpdateGoal(int id, [FromBody] Goal updatedGoal)
    {
    if (id != updatedGoal.GoalId)
    {
        return BadRequest("Goal ID cannot be changed");
    }

    if (_goalService.UpdateGoal(updatedGoal))
    {
        return Ok("Goal updated successfully");
    }
    else
    {
        return NotFound("Goal not found");
    }
    }


    [HttpDelete("{id}")]
    /*public IActionResult DeleteGoal(int id) =>
        _goalService.DeleteGoal(id) ? Ok("Goal deleted successfully") : NotFound("Goal not found");*/
    public IActionResult DeleteGoal(int id)
    {
    if (_goalService.DeleteGoal(id))
    {
        return Ok("Goal deleted successfully");
    }
    else
    {
        return NotFound("Goal not found");
    }
    }


}
