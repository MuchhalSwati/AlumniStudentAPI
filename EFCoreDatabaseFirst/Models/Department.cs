using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDatabaseFirst.Models
{
    public partial class Department
    {
        public Department()
        {
            InverseUniversity = new HashSet<Department>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int UniversityId { get; set; }

        public virtual Department University { get; set; }
        public virtual ICollection<Department> InverseUniversity { get; set; }
    }
}
