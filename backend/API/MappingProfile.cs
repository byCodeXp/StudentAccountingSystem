using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}