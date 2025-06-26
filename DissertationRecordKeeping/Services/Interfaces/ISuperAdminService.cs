using DissertationRecordKeeping.Models;

namespace DissertationRecordKeeping.Services.Interfaces;

public interface ISuperAdminService
{
    Task<Admin> RegisterAdmin(Admin admin);
    Task<Admin> GetAdmin(int id);
    Task<List<Admin>> GetAllAdmins();
    Task<Admin> UpdateAdmin(int id, Admin admin);
    Task<bool> DeleteAdmin(int id);
    Task<string> Login(LoginModel loginModel);
    Task<List<Admin>> GetAllAdmin();
}
