using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAdminPortal.API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Student> Student { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
