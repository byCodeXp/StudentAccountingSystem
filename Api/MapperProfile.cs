using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Requests;

namespace Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<RegisterRequest, User>();
        }
    }
}
