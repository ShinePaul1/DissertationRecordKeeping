using DissertationRecordKeeping.Data;
using DissertationRecordKeeping.Models;
using DissertationRecordKeeping.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DissertationRecordKeeping.Services;

public class SuperAdminService : ISuperAdminService
{
    private readonly RecordContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SuperAdminService> _logger;

    public SuperAdminService(
        RecordContext context,
        IConfiguration configuration,
        ILogger<SuperAdminService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Admin> RegisterAdmin(Admin registerModel)
    {
        try
        {
            _logger.LogInformation("Registering Admin {Username and School}", registerModel.Username, registerModel.School, registerModel.Email);

            if (await _context.Admins.AnyAsync(u => u.Username == registerModel.Username))
                throw new ValidationException("Username already exists");

            if (await _context.Admins.AnyAsync(u => u.Email == registerModel.Email))
                throw new ValidationException("Email already exists");

            var admin = new Admin
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Username = registerModel.Username,
                School = registerModel.School,
                ContactNumber = registerModel.ContactNumber,
                Email = registerModel.Email,
                Role = registerModel.Role,
                Password = HashPassword(registerModel.Password),
                CreatedAt = DateTime.Now
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Admin {Username and School} registered successfully", admin.Username, admin.School);
            return admin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering admin {Username and School}", registerModel.Username, registerModel.School);
            throw;
        }
    }
    public async Task<string> Login(Login loginModel)
    {
        try
        {
            _logger.LogInformation("Login attempt for {Username and School}", loginModel.UserName, loginModel.School);

            var admin = await _context.Admins
                .FirstOrDefaultAsync(u => u.Username == loginModel.UserName);

            if (admin == null || !VerifyPassword(loginModel.Password, admin.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJwtToken(admin);

            _logger.LogInformation("User {Username and School} logged in successfully", admin.Username, admin.School);
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for {Username and School}", loginModel.UserName, loginModel.School);
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

    private string GenerateJwtToken(Admin admin)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, admin.Username), // Username claim
            new Claim(ClaimTypes.Role, admin.Role)      // Role claim
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Admin> GetAdmin(int id)
    {
        try
        {
            _logger.LogInformation("Fetching admin with ID {Id}", id);
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
                throw new KeyNotFoundException("Admin not found");

            return admin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching admin with ID {Id}", id);
            throw;
        }
    }
    public async Task<List<Admin>> GetAllAdmins()
    {
        try
        {
            _logger.LogInformation("Fetching all admins");
            return await _context.Admins.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all admins");
            throw;
        }
    }
    public async Task<Admin> UpdateAdmin(int id, Admin admin)
    {
        try
        {
            _logger.LogInformation("Updating admin with ID {Id}", id);
            var existingAdmin = await _context.Admins.FindAsync(id);
            if (existingAdmin == null)
                throw new KeyNotFoundException("Admin not found");

            existingAdmin.FirstName = admin.FirstName;
            existingAdmin.LastName = admin.LastName;
            existingAdmin.Username = admin.School;
            existingAdmin.School = admin.School;
            existingAdmin.ContactNumber = admin.ContactNumber;
            existingAdmin.Email = admin.Email;
            existingAdmin.Role = admin.Role;
            existingAdmin.Password = admin.School;

            _context.Admins.Update(existingAdmin);
            await _context.SaveChangesAsync();

            return existingAdmin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating admin with ID {Id}", id);
            throw;
        }
    }
    public async Task<bool> DeleteAdmin(int id)
    {
        try
        {
            _logger.LogInformation("Deleting admin with ID {Id}", id);
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
                throw new KeyNotFoundException("Admin not found");

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting admin with ID {Id}", id);
            throw;
        }
    }

    public Task<List<Admin>> GetAllAdmin()
    {
        throw new NotImplementedException();
    }

    public Task LoginAsAdmin(object login)
    {
        throw new NotImplementedException();
    }

    public Task LoginAsAdmin(Login loginModel)
    {
        throw new NotImplementedException();
    }

}
