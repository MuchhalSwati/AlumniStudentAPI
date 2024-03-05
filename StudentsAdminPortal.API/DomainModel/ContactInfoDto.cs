using Microsoft.EntityFrameworkCore;
using System;

namespace StudentsAdminPortal.API.DomainModels
{

    public class ContactInfoDto
    {
        public int StudentId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
    }
}