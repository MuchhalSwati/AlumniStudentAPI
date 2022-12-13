using StudentsAdminPortal.API.Models;
using System.Collections.Generic;

namespace StudentsAdminPortal.Repository
{
    public interface IStudentRepository
    {
        List<Students> GetStudentData(int UniversityId, int StudentId);
        List<Students> GetListOfStudentsForDepartment(int UniversityId, int DepartmentId);
        void AddStudent(Student students);
        void AddContactInfo(ContactInfo ContactInfo);
    }
}