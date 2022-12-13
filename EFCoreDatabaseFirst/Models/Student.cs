using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDatabaseFirst.Models
{
    public partial class Student
    {
        public Student()
        {
            InverseDepartment = new HashSet<Student>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DepartmentId { get; set; }

        public virtual Student Department { get; set; }
        public virtual ICollection<Student> InverseDepartment { get; set; }
    }
}
