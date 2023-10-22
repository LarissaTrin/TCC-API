using Project.Domain.Identity;
using Project.Persistence.Models;
using Project.Persistence.Persistence;

namespace Project.Persistence.IPersistence
{
    public interface IProjectUserPersistence
    {
        Task<PageList<User>> GetAllUsersAsync(int userId, PageParams pageParams);
        Task<PageList<User>> GetAllUsersByEditAsync(int projectId, PageParams pageParams);
    }
}