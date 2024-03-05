using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsAdminPortal.API.DomainModels
{
    public class StudentDto
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastDate { get; set; }
        public int DepartmentId { get; set; }



        public StudentDto()
        {
            FirstName = "";
        }
    }
}
