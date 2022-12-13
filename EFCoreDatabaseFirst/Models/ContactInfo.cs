using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDatabaseFirst.Models
{
    public partial class ContactInfo
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public long? PhoneNumber { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
