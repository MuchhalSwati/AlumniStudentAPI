using AutoMapper;
using EFCoreDatabaseFirst.Models;
using FluentValidation;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StudentsAdminPortal.API.Caching;
using StudentsAdminPortal.API.DomainModels;
using StudentsAdminPortal.API.Models;
using StudentsAdminPortal.API.ServiceClass;
using System;
using System.Collections.Generic;
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
        private ICacheProvider _cacheProvider;
        public StudentController(IStudentServiceClass studentServiceClass, IMapper mapper, ICacheProvider cache)
        {
            _studentServiceClass = studentServiceClass;
            _mapper = mapper;
            _cacheProvider = cache;

        }

        [Route("{universityId}/{studentId}/studentCreditInfo")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsCreditRecord(int universityId, int studentId)
        {
            var studentRecord = await _studentServiceClass.GetStudentsData(universityId, studentId);
            
            return studentRecord != null ? Ok(studentRecord) : NotFound();

        }

        [Route("{universityId}/{departmentId}/department")]
        [HttpGet]
        public async Task<IActionResult> GetlistOfStudentsForDepartmentAsync(int universityId, int departmentId)
        {
            string cacheKey = Cachekeys.GetStudentCacheKey(universityId, departmentId);

            if (!_cacheProvider.TryGetValue(cacheKey, out List<Students> students))
            {
                students = await _studentServiceClass.GetStudentsListAsync(universityId, departmentId);
                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(60),
                    Size = 1024
                };
                _cacheProvider.Set(cacheKey, students, cacheEntryOption);
            }
            if (students.Count == 0)
                return NotFound();

            return Ok(new { Students = students });

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
        public async Task<IActionResult> EnterStudentRecord(StudentUpdate studentRecord)
        {
            if (studentRecord == null)
                return BadRequest();

            var addStud = _mapper.Map<Student>(studentRecord);
            var addCredits = _mapper.Map<Credits>(studentRecord);
            var addContact = _mapper.Map<ContactInfo>(studentRecord);
            await _studentServiceClass.AddStudentRecord(addStud, HttpContext);
            await _studentServiceClass.AddContactInfo(addContact, HttpContext);
            await _studentServiceClass.AddStudentCredits(addCredits, HttpContext);
            return Ok();
        }
       
        [Route("{universityId}/{studentId}/StudentUpdate")]
        [HttpPut]
        public async Task<IActionResult> updateStudentRecord(int universityId, int studentId, StudentUpdate studentUpdate)
        {
            var updateStudentRecord = await _studentServiceClass.GetStudentsData(universityId, studentId);
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

            await _studentServiceClass.AddStudentRecord(updateStud, HttpContext);
            await _studentServiceClass.AddStudentCredits(updateCredits, HttpContext);
            await _studentServiceClass.AddContactInfo(updateContact, HttpContext);

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

        [Route("{universityId}/{studentId}/StudentDelete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStudentRecord(int universityId, int studentId)
        {
            var studentRecordToDelete = await _studentServiceClass.GetStudentsData(universityId, studentId);
            if (studentRecordToDelete is null)
                return NotFound();

            var studentRecord = studentRecordToDelete.Select(s => new Student
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                StartDate = s.StartDate,
                LastDate = s.LastDate,
                DepartmentId = s.DepartmentId
            });

           await _studentServiceClass.DeleteStudent(studentRecord);
           return NoContent();

        }





    }
}
