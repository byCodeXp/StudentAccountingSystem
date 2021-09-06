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
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
            
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            
            CreateMap<RegisterRequest, User>();
        }
    }
}