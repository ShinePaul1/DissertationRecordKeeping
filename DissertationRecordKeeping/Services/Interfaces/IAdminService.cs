using DissertationRecordKeeping.Models;

namespace DissertationRecordKeeping.Services.Interfaces;

public interface IAdminService
{
    Task<StudentInformation> AddStudentInformation(StudentInformation student);
    Task<StudentInformation> GetStudentInformation(int matriculationNumber);
    Task<List<StudentInformation>> GetAllStudentInformations();
    Task<StudentInformation> UpdateStudentInformation(int matriculationNumber, StudentInformation student);
    Task<bool> DeleteStudentInformation(int matriculationNumber);
    //Task<string> Login(Login login);
    //Task LoginAsAdmin(object login);
    //Task LoginAsAdmin(Login loginModel);
}
