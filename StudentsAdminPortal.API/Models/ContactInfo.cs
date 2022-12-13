using Microsoft.EntityFrameworkCore;
using System;

namespace StudentsAdminPortal.API.Models
{
    
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long? PhoneNumber { get; set; }
        public int StudentId { get; set; }

        //Navigation property
        public Student Student { get; set; }
    }
}