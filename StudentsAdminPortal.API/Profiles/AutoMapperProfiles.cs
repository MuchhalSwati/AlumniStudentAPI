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
            CreateMap<StudentUpdate, Student>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.LastDate, opt => opt.MapFrom(src => src.LastDate))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));
            CreateMap<StudentUpdate, Credits>()
                .ForMember(dest => dest.FirstYear, opt => opt.MapFrom(src => src.FirstYear))
                .ForMember(dest => dest.SecondYear, opt => opt.MapFrom(src => src.SecondYear))
                .ForMember(dest => dest.ThirdYear, opt => opt.MapFrom(src => src.ThirdYear))
                .ForMember(dest => dest.FourthYear, opt => opt.MapFrom(src => src.FourthYear))
                .ForMember(dest => dest.FifthYear, opt => opt.MapFrom(src => src.FifthYear));
            CreateMap<StudentUpdate, ContactInfo>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }

        
        
    }
}
