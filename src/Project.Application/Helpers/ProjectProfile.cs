using AutoMapper;
using Project.Application.DTOs;
using Project.Domain;
using Project.Domain.Identity;

namespace Project.Application.Helpers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectList, ProjectListDTO>().ReverseMap();
            CreateMap<List, ListDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Tags, TagsDTO>().ReverseMap();
            CreateMap<TagCard, TagCardDTO>().ReverseMap();
            CreateMap<TaskProject, TaskProjectDTO>().ReverseMap();
            CreateMap<Approver, ApproverDTO>().ReverseMap();
            CreateMap<Comments, CommentsDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<ProjectUser, ProjectUserDTO>().ReverseMap();
        }
    }
}