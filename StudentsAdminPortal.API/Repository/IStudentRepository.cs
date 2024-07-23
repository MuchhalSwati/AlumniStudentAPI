using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsAdminPortal.Repository
{
    public interface IStudentRepository
    {
        List<Students> GetStudentData(int UniversityId, int StudentId);
        Task<List<Students>> GetListOfStudentsForDepartmentAsync(int UniversityId, int DepartmentId);
        int AddStudent(Student students, HttpContext context);
        void AddContactInfo(ContactInfo ContactInfo, HttpContext context);
        void AddStudentCredits(Credits credit, HttpContext context);
    }
}