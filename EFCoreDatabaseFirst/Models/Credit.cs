using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDatabaseFirst.Models
{
    public partial class Credit
    {
        public int? FirstYear { get; set; }
        public int? SecondYear { get; set; }
        public int? ThirdYear { get; set; }
        public int? FourthYear { get; set; }
        public int? FifthYear { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
