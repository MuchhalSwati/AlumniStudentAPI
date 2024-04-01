using AutoMapper;
using EFCoreDatabaseFirst.Models;
using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;
using StudentsAdminPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ContactInfo = StudentsAdminPortal.API.Models.ContactInfo;
using Student = StudentsAdminPortal.API.Models.Student;

namespace StudentsAdminPortal.API.ServiceClass
{
    public class StudentServiceClass : IStudentServiceClass
    {
        private readonly IStudentRepository _studentRepository;
        private List<StudentsAward> StudentsList = new List<StudentsAward>();
        int studentId = 0;



        public StudentServiceClass(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

        }

        public List<StudentsAward> GetStudentsData(int universityId, int studentId)
        {
            var StudentRecord = _studentRepository.GetStudentData(universityId, studentId);
            var Students = StudentsAward(StudentRecord);
            return Students;
        }

        public List<Students> GetStudentsList(int universityId, int departmentId)
        {
            var studentDetails = _studentRepository.GetListOfStudentsForDepartment(universityId, departmentId);
            return studentDetails;

        }

        public List<Students> ReturnStudentsByYear(int universityId, int deptId, DateTime year)
        {
            var studentDetails = _studentRepository.GetListOfStudentsForDepartment(universityId, deptId);
            var results = studentDetails.Where(e => e.StartDate.Equals(year)).ToList();
            return results;
        }

        public List<StudentsAward> StudentsAward(List<Students> student)
        {
           
            StudentsAward record;
            foreach (var studt in student)
            {
                string award = null;
                DateTime fourYears = studt.StartDate.AddYears(4);
                var threeYearsCredit = studt.FirstYear + studt.SecondYear + studt.ThirdYear;
                var FourYearCredit = studt.FirstYear + studt.SecondYear + studt.ThirdYear + studt.FourthYear;
                if (threeYearsCredit.Equals(120) && studt.FirstYear.Equals(40))
                {
                    award = Constants.Bachelors;
                    record = MappingStudentsAwardWithStudents(award, studt);
                    StudentsList.Add(record);
                }
                if (threeYearsCredit < 120 && FourYearCredit >= 120)
                {
                    award = Constants.Extended;
                    record = MappingStudentsAwardWithStudents(award, studt);
                    StudentsList.Add(record);
                }
                if (FourYearCredit < 120 && studt.LastDate.Equals(fourYears))
                {
                    award = Constants.Disqualified;
                    record = MappingStudentsAwardWithStudents(award, studt);
                    StudentsList.Add(record);
                }

                if (FourYearCredit < 120 && studt.LastDate != fourYears)
                {
                    award = Constants.Discontinued;
                    record = MappingStudentsAwardWithStudents(award, studt);
                    StudentsList.Add(record);
                }

                if(award == null)
                {
                    record = MappingStudentsAwardWithStudents(award, studt);
                    StudentsList.Add(record);
                }
                   
            }
            return StudentsList;
        }

        public StudentsAward MappingStudentsAwardWithStudents(string Award, Students S)
        {
            var Awards = new StudentsAward()
            {
                UnivId = S.UnivId,
                UnivName = S.UnivName,
                Id = S.Id,
                FirstName = S.FirstName,
                LastName = S.LastName,
                StartDate = S.StartDate,
                LastDate = S.LastDate,
                Address = S.Address,
                PhoneNumber = S.PhoneNumber,
                Email = S.Email,
                DepartmentId = S.DepartmentId,
                DeptName = S.DeptName,
                creditScoreId = S.CreditScoreId,
                 ContactInfoId = S.ContactInfoId,
                FirstYear = S.FirstYear,
                SecondYear = S.SecondYear,
                ThirdYear = S.ThirdYear,
                FourthYear = S.FourthYear,
                FifthYear = S.FifthYear,
                Award = Award


            };

            return Awards;
        }

        public void AddStudentRecord(Student student, HttpContext context)
        {
            studentId = _studentRepository.AddStudent(student, context);
        }

        public void AddContactInfo(ContactInfo contact, HttpContext context)
        {
            contact.StudentId = studentId;
            _studentRepository.AddContactInfo(contact, context);
        }

        public void AddStudentCredits(Credits credit, HttpContext context)
        {
            credit.StudentId = studentId;
            _studentRepository.AddStudentCredits(credit, context);
        }
    }
}
