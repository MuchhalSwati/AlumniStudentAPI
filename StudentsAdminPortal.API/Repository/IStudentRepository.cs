using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;
using System.Collections.Generic;

namespace StudentsAdminPortal.Repository
{
    public interface IStudentRepository
    {
        List<Students> GetStudentData(int UniversityId, int StudentId);
        List<Students> GetListOfStudentsForDepartment(int UniversityId, int DepartmentId);
        void AddStudent(Student students, HttpContext context);
        void AddContactInfo(ContactInfo ContactInfo);
        void AddStudentCredits(Credits credit);
    }
}