using DissertationRecordKeeping.Services.Interfaces;
using DissertationRecordKeeping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using DissertationRecordKeeping.Services;

namespace DissertationRecordKeeping.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    //[Authorize(Roles = "admin")]
    [HttpPost("addStudentInformation")]
    public async Task<ActionResult<StudentInformation>> AddStudentInformation([FromBody] StudentInformation addStudentInformation)
    {
        if (!ModelState.IsValid)
            return Ok(ModelState);
        var student = await _adminService.AddStudentInformation(addStudentInformation);
        return CreatedAtAction(nameof(AddStudentInformation), new { matriculationNumber = student.MatriculationNumber, school = student.School }, student);
    }

    /*
    //[Authorize(Roles = "admin")]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsAdmin([FromBody] Login loginModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _adminService.Login(loginModel);
        return Ok(new { Token = token, username = loginModel.UserName, school = loginModel.School, status = 2 });
    }
    */
    //[Authorize(Roles = "admin")]
    [HttpGet("getstudentInformation/{matriculationNumber}")]
    public async Task<ActionResult<StudentInformation>> GetStudentInformation(int matriculationNumber)
    {
        var student = await _adminService.GetStudentInformation(matriculationNumber);
        if (student == null)
            return NotFound();

        return Ok(student);
    }
    [Authorize(Roles = "admin")]
    [HttpGet("getallstudentInformation")]
    public async Task<ActionResult<List<StudentInformation>>> GetAllStudentInformations()
    {
        var students = await _adminService.GetAllStudentInformations();
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
