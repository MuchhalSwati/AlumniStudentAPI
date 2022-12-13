using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAdminPortal.API.Models
{
    class AwardStatus 
    {
        public static string Bachelors = "Bachelors Degree";
        public static string Extended = "Extended Degree";
        public static string Disqualified = "Disqualified";
        public static string Discontinued = "Discontinued";
    }

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastDate { get; set; }
      
        //Navigation Property
       public ContactInfo ContactInfo { get; set; }
       public Credits Credits { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public string GetGraduationStatus()
        {
            DateTime FourYears = StartDate.AddYears(4);
            var ThreeYearsCredit = Credits.FirstYear + Credits.SecondYear + Credits.ThirdYear;
            var FourYearCredit = Credits.FirstYear + Credits.SecondYear + Credits.ThirdYear + Credits.FourthYear;
            if (ThreeYearsCredit.Equals(120) && Credits.FirstYear.Equals(40))
            {
                return Constants.Bachelors;
                //StudentsAward Record = MappingStudentsAwardWithStudents(Constants.Bachelors, S);
                //StudentsList.Add(Record);
            }
            if (ThreeYearsCredit < 120 && FourYearCredit >= 120)
            {
                return Constants.Extended;
                //StudentsAward Record = MappingStudentsAwardWithStudents(Constants.Extended, S);
                //StudentsList.Add(Record);
            }
            if (FourYearCredit < 120 && LastDate.Equals(FourYears))
            {
                return Constants.Disqualified;
                //StudentsAward Record = MappingStudentsAwardWithStudents(Constants.Disqualified, S);
                //StudentsList.Add(Record);
            }

            if (FourYearCredit < 120 && LastDate != FourYears)
            {
                return Constants.Discontinued;
                //StudentsAward Record = MappingStudentsAwardWithStudents(Constants.Discontinued, S);
                //StudentsList.Add(Record);
            }
            return null;
        }
       


        public Student()
        {
            FirstName = "";
        }
    }
}
