using System;
using System.Collections.Generic;
using StudentsAdminPortal.API.Models;


namespace StudentsAdminPortal.API.ServiceClass
{
    public interface IStudentServiceClass
    {
        List<StudentsAward> GetStudentsData(int universityId, int studentId);
        List<Students> GetStudentsList(int universityId, int departmentId);
        List<Students> ReturnStudentsByYear(int universityId, int departmentId, DateTime year);
        List<StudentsAward> StudentsAward(List<Students> student);
        void AddStudentRecord(Student students);
        void AddContactInfo(ContactInfo contact);
        
    }
}