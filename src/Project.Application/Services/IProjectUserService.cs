using Project.Application.DTOs;
using Project.Persistence.Models;

namespace Project.Application.Services
{
    public interface IProjectUserService
    {
        Task<ProjectUserDTO[]> SaveUsersByProject(int projectId, ProjectUserDTO[] models);
        Task<PageList<UserDTO>> GetAllUsersAsync(int userId, PageParams pageParams);
        Task<PageList<UserDTO>> GetAllUsersByEditAsync(int projectId, PageParams pageParams);
    }
}