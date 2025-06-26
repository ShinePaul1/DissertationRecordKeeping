using DissertationRecordKeeping.Services.Interfaces;
using DissertationRecordKeeping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DissertationRecordKeeping.Services;

namespace DissertationRecordKeeping.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SuperAdminController : ControllerBase
{
    private readonly ISuperAdminService _superAdminService;

    public SuperAdminController(ISuperAdminService superAdminService)
    {
        _superAdminService = superAdminService;
    }
   // [Authorize(Roles = "admin")]
    [HttpPost("registerAdmin")]
    public async Task<ActionResult<Admin>> RegisterAdmin([FromBody] Admin registerAdmin)
    {
        if (!ModelState.IsValid)
            return Ok(ModelState);
        var admin = await _superAdminService.RegisterAdmin(registerAdmin);
        return CreatedAtAction(nameof(LoginAsAdmin), new { username = admin.Username, school = admin.School }, admin);
    }
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsAdmin([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _superAdminService.Login(loginModel);
        return Ok(new { Token = token, username = loginModel.UserName, school = loginModel.School, status = 2 });
    }
    
    [HttpGet("getadmin/{id}")]
    public async Task<ActionResult<Admin>> GetAdmin(int id)
    {
        var admin = await _superAdminService.GetAdmin(id);
        if (admin == null)
            return NotFound();

        return Ok(admin);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("getalladmins")]
    public async Task<ActionResult<List<Admin>>> GetAllAdmin()
    {
        var admins = await _superAdminService.GetAllAdmins();
        return Ok(admins);
    }

    //[Authorize(Roles = "superadmin")]
    [HttpPut("updateadmin/{id}")]
    public async Task<ActionResult<Admin>> UpdateAdmin(int id, [FromBody] Admin admin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedAdmin = await _superAdminService.UpdateAdmin(id, admin);
        if (updatedAdmin == null)
            return NotFound();

        return Ok(updatedAdmin);
    }

    //[Authorize(Roles = "superadmin")]
    [HttpDelete("deleteadmin/{id}")]
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var result = await _superAdminService.DeleteAdmin(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
