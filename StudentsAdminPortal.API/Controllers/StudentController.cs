using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentsAdminPortal.API.DomainModels;
using StudentsAdminPortal.API.Models;
using StudentsAdminPortal.API.ServiceClass;
using System;

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
            
            if (studentRecord.Count == 0)
                return NotFound();
            return Ok(studentRecord);


        }

        [Route("{universityId}/{departmentId}/department")]
        [HttpGet]
        public IActionResult GetlistOfStudentsForDepartment(int universityId, int departmentId)
        {
            var stud = _studentServiceClass.GetStudentsList(universityId, departmentId);
            if (stud.Count == 0)
                return NotFound();

            return Ok(new { Students = stud });

        }

        [Route("{universityId}/{departmentId}/year")]
        [HttpGet]
        public IActionResult GetlistOfStudentsForDepartmentAndYear(int universityId, int departmentId, DateTime year)
        {

            var studentDetails = _studentServiceClass.ReturnStudentsByYear(universityId, departmentId, year);
            var stud = _studentServiceClass.StudentsAward(studentDetails);
            if (stud.Count == 0)
                return NotFound();

            return Ok(stud);
        }


        [HttpPost("studentRecord")]
        public IActionResult EnterStudentRecord(StudentDto studentRecord)
        {
            if (studentRecord == null)
                return BadRequest();

            //Map from DTO to Data model
            var studentModel = _mapper.Map<Student>(studentRecord);
            _studentServiceClass.AddStudentRecord(studentModel);

            //Map from Data model to DTO Model
            //var StudentDto = _mapper.Map<StudentDto>(StudentModel);
            //return 
            return Ok(studentModel);
        }

        [HttpPost("ContactInfo")]
        public IActionResult EnterContactInfo(ContactInfoDto contactInfoRecord)
        {
            if (contactInfoRecord == null)
                return BadRequest();

            //Map from DTO to Data model
            var contactModel = _mapper.Map<ContactInfo>(contactInfoRecord);
            _studentServiceClass.AddContactInfo(contactModel);

            return Ok(contactModel);
        }
    }
}
