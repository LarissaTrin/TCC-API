using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface ITagsPersistence
    {
        // TAGS
        Task<Tags[]> GetAllTagsByProjectIdAsync(string projectId, bool includeProject);
        Task<Tags[]> GetAllTagsByCardIdAsync(string CardId, bool includeProject);
        Task<Tags[]> GetAllTagsAsync(bool includeProject);
        Task<Tags> GetTagByIdAsync(int TagId, bool includeProject);
    }
}