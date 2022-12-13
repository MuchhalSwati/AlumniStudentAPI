using AutoMapper;
using StudentsAdminPortal.API.DomainModels;
using StudentsAdminPortal.API.Models;
/////using DatModel = StudentsAdminPortal.API.DomainModels;


namespace StudentsAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
            CreateMap<ContactInfoDto, ContactInfo>().ReverseMap();
     
        }
    }
}
