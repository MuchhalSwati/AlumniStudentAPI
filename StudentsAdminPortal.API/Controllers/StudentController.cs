using AutoMapper;
using EFCoreDatabaseFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentsAdminPortal.API.DomainModels;
using StudentsAdminPortal.API.Models;
using StudentsAdminPortal.API.ServiceClass;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ContactInfo = StudentsAdminPortal.API.Models.ContactInfo;
using Student = StudentsAdminPortal.API.Models.Student;

namespace StudentsAdminPortal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentServiceClass _studentServiceClass;
        private readonly IMapper _mapper;

        public StudentController(IStudentServiceClass studentServiceClass, IMapper mapper)
        {
            _studentServiceClass = studentServiceClass;
            _mapper = mapper;

        }

        [Route("{universityId}/{studentId}/studentCreditInfo")]
        [HttpGet]
        public IActionResult GetStudentsCreditRecord(int universityId, int studentId)
        {
            var studentRecord = _studentServiceClass.GetStudentsData(universityId, studentId);
            
            return studentRecord != null ? Ok(studentRecord) : NotFound();

        }

        [Route("{universityId}/{departmentId}/department")]
        [HttpGet]
        public async Task<IActionResult> GetlistOfStudentsForDepartmentAsync(int universityId, int departmentId)
        {
            var stud = await _studentServiceClass.GetStudentsListAsync(universityId, departmentId);
            if (stud.Count == 0)
                return NotFound();

            return Ok(new { Students = stud });

        }

        [Route("{universityId}/{departmentId}/year")]
        [HttpGet]
        public async Task<IActionResult> GetlistOfStudentsForDepartmentAndYear(int universityId, int departmentId, DateTime year)
        {

            var studentDetails = await _studentServiceClass.ReturnStudentsByYearAsync(universityId, departmentId, year);
            var stud = _studentServiceClass.StudentsAward(studentDetails);
            if (stud.Count == 0)
                return NotFound();

            return Ok(stud);
        }


        [HttpPost("studentRecord")]
        public IActionResult EnterStudentRecord(StudentUpdate studentRecord)
        {
            throw new Exception();

            if (studentRecord == null)
                return BadRequest();

            //Map from DTO to Data model
            //var studentModel = _mapper.Map<Student>(studentRecord);

            var addStud = _mapper.Map<Student>(studentRecord);
            var addCredits = _mapper.Map<Credits>(studentRecord);
            var addContact = _mapper.Map<ContactInfo>(studentRecord);
            _studentServiceClass.AddStudentRecord(addStud, HttpContext);
            _studentServiceClass.AddContactInfo(addContact, HttpContext);
            _studentServiceClass.AddStudentCredits(addCredits, HttpContext);
            return Ok();
        }

        //[HttpPost("ContactInfo")]
        //public IActionResult EnterContactInfo(ContactInfoDto contactInfoRecord)
        //{
        //    if (contactInfoRecord == null)
        //        return BadRequest();

        //    //Map from DTO to Data model
        //    var contactModel = _mapper.Map<ContactInfo>(contactInfoRecord);
        //    _studentServiceClass.AddContactInfo(contactModel, HttpContext);

        //    return Ok(contactModel);
        //}
        [Route("{universityId}/{studentId}/StudentUpdate")]
        [HttpPut]
        public IActionResult updateStudentRecord(int universityId, int studentId, StudentUpdate studentUpdate)
        {
            var updateStudentRecord = _studentServiceClass.GetStudentsData(universityId, studentId);
            if (updateStudentRecord is null)
            {
                return NotFound();
            }
            var updateStud = _mapper.Map<Student>(studentUpdate);
            var updateCredits = _mapper.Map<Credits>(studentUpdate);
            var updateContact = _mapper.Map<ContactInfo>(studentUpdate);
            updateStud.Id = studentId;
            updateCredits.StudentId = studentId;
            updateCredits.Id = updateStudentRecord.Select(e => e.creditScoreId).Single();
            updateContact.StudentId = studentId;
            updateContact.Id = updateStudentRecord.Select(e => e.ContactInfoId).Single();

            _studentServiceClass.AddStudentRecord(updateStud, HttpContext);
            _studentServiceClass.AddStudentCredits(updateCredits, HttpContext);
            _studentServiceClass.AddContactInfo(updateContact, HttpContext);

            var response = new Students
            {
                FirstName = updateStud.FirstName,
                LastName = updateStud.LastName,
                DepartmentId = updateStud.DepartmentId,
                StartDate = updateStud.StartDate,
                LastDate = updateStud.LastDate,
                CreditScoreId = updateCredits.Id,
                FirstYear = updateCredits.FirstYear,
                SecondYear = updateCredits.SecondYear,
                ThirdYear = updateCredits.ThirdYear,
                FourthYear = updateCredits.FourthYear,
                FifthYear = updateCredits.FifthYear,
                Address = updateContact.Address,
                PhoneNumber = updateContact.PhoneNumber,
                 Email = updateContact.Email,
                 Id = updateStud.Id

            };
            return Ok(response);
        }


    }
}
