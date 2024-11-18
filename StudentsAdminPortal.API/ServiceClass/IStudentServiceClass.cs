using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;


namespace StudentsAdminPortal.API.ServiceClass
{
    public interface IStudentServiceClass
    {
        Task<List<StudentsAward>> GetStudentsData(int universityId, int studentId);
        Task<List<Students>> GetStudentsListAsync(int universityId, int departmentId);
        Task<List<Students>> ReturnStudentsByYearAsync(int universityId, int departmentId, DateTime year);
        List<StudentsAward> StudentsAward(List<Students> student);
        Task<int> AddStudentRecord(Student students, HttpContext method);
        Task AddContactInfo(ContactInfo contact, HttpContext method);
        Task AddStudentCredits(Credits credits, HttpContext method);
        Task DeleteStudent(IEnumerable<Student> student);
        //Task DeleteStudent(IEnumerable<Credits> credit);
        //Task DeleteStudent(IEnumerable<ContactInfo> contactInfo);

    }
}