using System.Collections.Generic;

namespace StudentsAdminPortal.API.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Department> Department { get; set; }
    }
}
