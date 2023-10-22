using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface IListPersistence
    {
        // LISTS
        Task<List[]> GetListsByProjectIdAsync(int projectId);
        Task<List> GetListByIdsAsync(int projectId, int ListId);
    }
}