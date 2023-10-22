using Project.Application.DTOs;

namespace Project.Application.Services
{
    public interface IListService
    {
        Task<ListDTO[]> SaveLists(int projectId, ListDTO[] models);
        Task<bool> DeleteList(int projectId, int listId);

        Task<ListDTO[]> GetListsByProjectIdAsync(int projectListId);
        Task<ListDTO> GetListByIdsAsync(int projectId, int ListId);
    }
}