using DissertationRecordKeeping.Models;

namespace DissertationRecordKeeping.Services.Interfaces;

public interface ISuperAdminService
{
    Task<Admin> RegisterAdmin(Admin admin);
    Task<Admin> GetAdmin(int id);
    Task<List<Admin>> GetAllAdmin();
    Task<Admin> UpdateAdmin(int id, Admin admin);
    Task<bool> DeleteAdmin(int id);
    Task<string> Login(Login login);
    Task LoginAsAdmin(object login);
    Task LoginAsAdmin(Login loginModel);
}
