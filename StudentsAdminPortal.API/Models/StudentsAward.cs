using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAdminPortal.API.Models
{
    public class StudentsAward
    {
        public int Id { get; set; }
        public int UnivId { get; set; }
        public string UnivName { get; set; }
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastDate { get; set; }
        public int? FirstYear { get; set; }
        public int? SecondYear { get; set; }
        public int? ThirdYear { get; set; }
        public int? FourthYear { get; set; }
        public int? FifthYear { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long? PhoneNumber { get; set; }
        public string Award { get; set; }
    }
 
    
}
