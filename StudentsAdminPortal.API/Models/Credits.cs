using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAdminPortal.API.Models
{

    public class Credits
    {
        public int Id { get; set; }
        public int? FirstYear { get; set; }
        public int? SecondYear { get; set; }
        public int? ThirdYear { get; set; }
        public int? FourthYear { get; set; }
        public int? FifthYear { get; set; }
        public int StudentId { get; set; }

        //Navigation property
        public Student Student { get; set; }

    }
}
