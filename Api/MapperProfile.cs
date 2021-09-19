using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;

namespace Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
                .ForMember(d => d.RegisterAt, s => s.MapFrom(v => v.CreatedTimeStamp))
                .ForMember(d => d.Courses, s => s.MapFrom(v => v.SubscribedCourses))
                .ReverseMap();
            
            CreateMap<RegisterRequest, User>();
        }
    }
}