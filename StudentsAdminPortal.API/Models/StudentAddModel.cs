using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAdminPortal.API.Models
{
    public class StudentAddModel
    {
        public University University { get; set; }
        public Department Department { get; set; }
        public Student Student { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public Credits Credits { get; set; }
    }
}
