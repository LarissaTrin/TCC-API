using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface IProjectListPersistence
    {
        // PROJECTS
        Task<ProjectList[]> GetAllProjectsByUserAsync(int userId, string user, bool includeUsers, bool includeTags, bool includeLists);
        Task<ProjectList[]> GetAllProjectsAsync(int userId);
        Task<ProjectList> GetProjectByIdAsync(int userId, int projectId);
        Task<ProjectList> GetProjectByIdForDeleteAsync(int userId, int projectId);
        Task<ProjectUser[]> GetUsersByProjectByIdAsync(int projectId);
        Task<ProjectUser>GetUserByIdByProjectAsync(int projectId, int userId);
    }
}