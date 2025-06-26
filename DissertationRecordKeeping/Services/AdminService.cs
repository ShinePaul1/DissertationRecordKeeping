using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DissertationRecordKeeping.Data;
using DissertationRecordKeeping.Models;
using DissertationRecordKeeping.Services.Interfaces;

namespace DissertationRecordKeeping.Services;

public class AdminService : IAdminService
{
    private readonly RecordContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AdminService> _logger;

    public AdminService(
        RecordContext context,
        IConfiguration configuration,
        ILogger<AdminService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<StudentInformation> AddStudentInformation(StudentInformation addModel)
    {
        try
        {
            _logger.LogInformation("Adding Student {MatriculationNumber}", addModel.MatriculationNumber, addModel.Email, addModel.School);

            if (await _context.StudentInformations.AnyAsync(u => u.MatriculationNumber == addModel.MatriculationNumber))
                throw new ValidationException("Matriculation Number already exists");

            if (await _context.StudentInformations.AnyAsync(u => u.Email == addModel.Email))
                throw new ValidationException("Email already exists");


            var student = new StudentInformation
            {
                FirstName = addModel.FirstName,
                LastName = addModel.LastName,
                School = addModel.School,
                Department = addModel.Department,
                Program = addModel.Program,
                Email = addModel.Email,
                MatriculationNumber = addModel.MatriculationNumber,
                Supervisor = addModel.Supervisor,
                DocumentType = addModel.DocumentType,
                DocumentTitle = addModel.DocumentTitle,
                Level = addModel.Level,
               Role= addModel.Role,
                CreatedAt = DateTime.Now
            };

            _context.StudentInformations.Add(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Student {MatriculationNumber} added successfully", student.MatriculationNumber, student.School);
            return student;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding student record {MatriculationNumber}", addModel.MatriculationNumber, addModel.School);
            throw;
        }
    }
    /*
    public async Task<string> Login(LoginModel loginModel)
    {
        try
        {
            _logger.LogInformation("Login attempt for {Username}", loginModel.Password, loginModel.UserName, loginModel.School);

            var admin = await _context.StudentInformations
                .FirstOrDefaultAsync(u => u.School == loginModel.School);

            if (admin == null || !VerifyPassword(loginModel.Password, admin.Password))
                throw new UnauthorizedAccessException("Invalid credentials");
            if (admin == null || !VerifyPassword(loginModel.UserName, admin.Username))
                throw new UnauthorizedAccessException("Invalid credentials");
            if (admin == null || !VerifyPassword(loginModel.School, admin.School))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJwtToken(admin);

            _logger.LogInformation("User {Username} logged in successfully", admin.Username, admin.School);
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for {Username}", loginModel.Password, loginModel.UserName, loginModel.School);
            throw;
        }
    }
    
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
    private bool VerifyPassword(string password, string hash)
    {
        var computedHash = HashPassword(password);
        return computedHash == hash;
    }
private string GenerateJwtToken(StudentInformation student)
{
var securityKey = new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

var claims = new[]
{
new Claim(ClaimTypes.Name, student.School), // Username claim
new Claim(ClaimTypes.Role, student.Role)      // Role claim
};

var token = new JwtSecurityToken(
issuer: _configuration["Jwt:Issuer"],
audience: _configuration["Jwt:Audience"],
claims: claims,
expires: DateTime.Now.AddHours(1),
signingCredentials: credentials);

return new JwtSecurityTokenHandler().WriteToken(token);
}
    */
    public Task<StudentInformation> UpdateStudentInformation(int matriculationNumber, StudentInformation student)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteStudentInformation(int matriculationNumber)
    {
        throw new NotImplementedException();
    }
    public Task<StudentInformation> GetStudentInformation(int matriculationNumber)
    {
        throw new NotImplementedException();
    }
    public Task<List<StudentInformation>> GetAllStudentInformations()
    {
        throw new NotImplementedException();
    }
}