using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsAdminPortal.Repository
{
    public interface IStudentRepository
    {
        Task <List<Students>> GetStudentData(int UniversityId, int StudentId);
        Task<List<Students>> GetListOfStudentsForDepartmentAsync(int UniversityId, int DepartmentId);
        Task<int> AddStudent(Student students, HttpContext context);
        Task AddContactInfo(ContactInfo ContactInfo, HttpContext context);
        Task AddStudentCredits(Credits credit, HttpContext context);
        Task DeleteStudentRecord(IEnumerable<Student> student);
        //Task DeleteStudentRecord(IEnumerable<Credits> student);
        //Task DeleteStudentRecord(IEnumerable<ContactInfo> student);
    }
}