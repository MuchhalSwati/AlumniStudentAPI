using StudentsAdminPortal.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAdminPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentsDbContext _studentDbContext;

        public StudentRepository(StudentsDbContext studentDbcontext)
        {
            _studentDbContext = studentDbcontext;
        }

        public List<Students> GetListOfStudentsForDepartment(int universityId, int departmentId)
        {
            return (from u in _studentDbContext.University
                    join d in _studentDbContext.Department on u.Id equals d.UniversityId
                    join s in _studentDbContext.Student on d.Id equals s.Department.Id
                    join c in _studentDbContext.Credits on s.Id equals c.StudentId into cr
                    from c in cr.DefaultIfEmpty()
                    join a in _studentDbContext.ContactInfo on s.Id equals a.StudentId into cont
                    from a in cont.DefaultIfEmpty()
                    where d.Id == departmentId && u.Id == universityId
                    select new Students
                    {
                        UnivId = u.Id,
                        UnivName = u.Name,
                        DepartmentId = d.Id,
                        DeptName = d.Name,
                        Id = s.Id,
                        StartDate = s.StartDate,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        LastDate = s.LastDate,
                        FirstYear = c == null ? null : c.FirstYear,
                        SecondYear = c == null ? null : c.SecondYear,
                        ThirdYear = c == null ? null : c.ThirdYear,
                        FourthYear = c == null ? null : c.FourthYear,
                        FifthYear = c == null ? null : c.FifthYear,
                        Address = a == null ? null : a.Address,
                        Email = a == null ? null : a.Email,
                        PhoneNumber = a == null ? null : a.PhoneNumber

                    }).ToList();
        }

        public void AddStudent(Student student)
        {
            _studentDbContext.Student.Add(student);
            _studentDbContext.SaveChanges();
        }
        public void AddContactInfo(ContactInfo contactInfo)
        {
            _studentDbContext.ContactInfo.Add(contactInfo);
            _studentDbContext.SaveChanges();
        }

        public List<Students> GetStudentData(int universityId, int studentId)
        {
            var stud = (from u in _studentDbContext.University
                        join d in _studentDbContext.Department on u.Id equals d.UniversityId
                        join s in _studentDbContext.Student on d.Id equals s.Department.Id
                        join c in _studentDbContext.Credits on s.Id equals c.StudentId into cr
                        from c in cr.DefaultIfEmpty()
                        join a in _studentDbContext.ContactInfo on s.Id equals a.StudentId into cont
                        from a in cont.DefaultIfEmpty()
                        where s.Id == studentId && u.Id == universityId
                        select new Students
                        {
                            UnivId = u.Id,
                            UnivName = u.Name,
                            DepartmentId = d.Id,
                            DeptName = d.Name,
                            Id = s.Id,
                            StartDate = s.StartDate,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            LastDate = s.LastDate,
                            FirstYear = c == null ? null : c.FirstYear,
                            SecondYear = c == null ? null : c.SecondYear,
                            ThirdYear = c == null ? null : c.ThirdYear,
                            FourthYear = c == null ? null : c.FourthYear,
                            FifthYear = c == null ? null : c.FifthYear,
                            Address = a == null ? null : a.Address,
                            Email = a == null ? null : a.Email,
                            PhoneNumber = a == null ? null : a.PhoneNumber

                        }).ToList();
            return stud;
        }
    }
}


//var StudentAdd = new Students
//{
//    University = new University
//    {
//        Id = Student.UnivId,
//        Name = Student.UnivName,
//        Department = new List<Department>
//                     {
//                       new Department {
//                           Id = Student.DepartmentId,
//                           Name = Student.DeptName,
//                          // UniversityId = Student.Department.UniversityId,
//                           Student = new List<Student>
//                          {
//                                 new Student
//                                 {
//                                      FirstName = Student.FirstName,
//                                      LastName = Student.LastName,
//                                      StartDate = Student.StartDate,
//                                      LastDate = Student.LastDate,
//                                      ContactInfo = new ContactInfo
//                                      {
//                                         //StudentId = Student.ContactInfo.Id,
//                                         Address = Student.Address,
//                                         Email = Student.Email,
//                                         PhoneNumber = Student.PhoneNumber,
//                                     },

//                                       Credits = new Credits
//                                     {
//                                         FirstYear =  Student.FirstYear,
//                                         SecondYear = Student.SecondYear,
//                                         ThirdYear = Student.ThirdYear,
//                                         FourthYear = Student.FourthYear,
//                                         FifthYear = Student.FifthYear,
//                                    }

//                                 }
//                           }
//                }
//            }
//    }
//};