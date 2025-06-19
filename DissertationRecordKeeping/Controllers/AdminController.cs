using DissertationRecordKeeping.Services.Interfaces;
using DissertationRecordKeeping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Identity;

namespace DissertationRecordKeeping.Controllers;


[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
   // [Authorize(Roles = "admin" )]
    [HttpPost("addStudentInformation")]
    public async Task<ActionResult<List<StudentInformation>>> AddStudentInformation(StudentInformation addModel)
    {
        if (!ModelState.IsValid)
            return Ok(await _adminService.GetAllStudentInformation());

        var student = await _adminService.AddStudentInformation(addModel);
        return CreatedAtAction(nameof(Login), new { school = student.School }, student);
    }

    //[Authorize(Roles = "admin")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login loginModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _adminService.Login(loginModel);
        return Ok(new { Token = token, username = loginModel.UserName, status = 1 });
    }
    //[Authorize(Roles = "admin")]
    [HttpGet("getstudentInformation/{id}")]
    public async Task<ActionResult<StudentInformation>> GetStudentInformation(int id)
    {
        var student = await _adminService.GetStudentInformation(id);
        if (student == null)
            return NotFound();

        return Ok(student);
    }
    [Authorize(Roles = "admin")]
    [HttpGet("getallstudentInformation")]
    public async Task<ActionResult<List<StudentInformation>>> GetAllStudentInformation()
    {
        var students = await _adminService.GetAllStudentInformation();
        return Ok(students);
    }
    //[Authorize(Roles = "Admin")]
    [HttpPut("updatestudentInformation/{matriculationNumber}")]
    public async Task<ActionResult<StudentInformation>> UpdateStudentInformation(int matriculationNumber, [FromBody] StudentInformation student)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedStudentInformation = await _adminService.UpdateStudentInformation(matriculationNumber,  student);
        if (updatedStudentInformation == null)
            return NotFound();

        return Ok(updatedStudentInformation);
    }
    //[Authorize(Roles = "Admin")]
    [HttpDelete("deletestudentInformation/{matriculationNumber}")]
    public async Task<IActionResult> DeletestudentInformation(int matriculationNumber)
    {
        var result = await _adminService.DeleteStudentInformation(matriculationNumber);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
