using DissertationRecordKeeping.Models;

namespace DissertationRecordKeeping.Services.Interfaces;

public interface IAdminService
{
    Task<StudentInformation> AddStudentInformation(StudentInformation addModel);
    Task<StudentInformation> GetStudentInformation(int id);
    Task<List<StudentInformation>> GetAllStudentInformation();
    Task<StudentInformation> UpdateStudentInformation(int matriculationNumber, StudentInformation student);
    Task<bool> DeleteStudentInformation(int id);
    Task<string> Login(Login login);
}
