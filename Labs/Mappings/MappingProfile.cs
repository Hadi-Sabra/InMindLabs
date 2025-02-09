using AutoMapper;
using Lab1.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping from User to Student
        CreateMap<User, Student>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id)) // Map Id to StudentId
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name)) // Map Name to FullName
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Map Email to Email
            .ForMember(dest => dest.Age, opt => opt.Ignore()); 

        // Mapping from Student to User
        CreateMap<Student, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId)) // Map StudentId to Id
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName)) // Map FullName to Name
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Map Email to Email

    }
}