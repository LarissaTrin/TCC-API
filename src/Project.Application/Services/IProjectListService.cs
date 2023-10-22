using Project.Application.DTOs;

namespace Project.Application.Services
{
    public interface IProjectListService
    {
        Task<ProjectListDTO> AddProject(int userId, ProjectListDTO model);
        Task<ProjectListDTO> UpdateProject(int userId, int projectId, ProjectListDTO model);
        Task<bool> DeleteProject(int userId, int projectId);

        Task<ProjectUserDTO[]> SaveUsersByProject(int projectId, IEnumerable<ProjectUserDTO> users);
        Task<ProjectListDTO[]> GetAllProjectsByUserAsync(int userId, string user, bool includeUsers = false, bool includeTags = false, bool includeLists = false);
        Task<ProjectListDTO[]> GetAllProjectsAsync(int userId, bool includeUsers = false, bool includeTags = false, bool includeLists = false);
        Task<ProjectListDTO> GetProjectByIdAsync(int userId, int projectId);
    }
}