using DissertationRecordKeeping.Services.Interfaces;
using DissertationRecordKeeping.Models;
// using DissertationRecordKeeping.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DissertationRecordKeeping.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<Admin>> Register([FromBody] Admin registerModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var admin = await _authService.Register(registerModel);
        return CreatedAtAction(nameof(Login), new { username = admin.Username }, admin);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login loginModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.Login(loginModel);
        return Ok(new { Token = token, username = loginModel.Username, status = 1 });
    }
    [HttpGet("getadmin/{id}")]
    public async Task<ActionResult<Admin>> GetAdmin(int id)
    {
        var admin = await _authService.GetAdmin(id);
        if (admin == null)
            return NotFound();

        return Ok(admin);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("getalladmin")]
    public async Task<ActionResult<List<Admin>>> GetAllAdmin()
    {
        var admins = await _authService.GetAllAdmin();
        return Ok(admins);
    }
    [HttpPut("updateadmin/{id}")]
    public async Task<ActionResult<Admin>> UpdateAdmin(int id, [FromBody] Admin admin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedAdmin = await _authService.UpdateAdmin(id, admin);
        if (updatedAdmin == null)
            return NotFound();

        return Ok(updatedAdmin);
    }
    [HttpDelete("deleteadmin/{id}")]
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var result = await _authService.DeleteAdmin(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}