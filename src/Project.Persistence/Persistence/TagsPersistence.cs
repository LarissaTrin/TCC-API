using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class TagsPersistence : ITagsPersistence
    {
        public DataContext _context { get; }
        public TagsPersistence(DataContext context)
        {
            _context = context;
            
        }

        public Task<Tags[]> GetAllTagsAsync(bool includeProject)
        {
            throw new NotImplementedException();
        }

        public Task<Tags[]> GetAllTagsByCardIdAsync(string CardId, bool includeProject)
        {
            throw new NotImplementedException();
        }

        public Task<Tags[]> GetAllTagsByProjectIdAsync(string projectId, bool includeProject)
        {
            throw new NotImplementedException();
        }

        public Task<Tags> GetTagByIdAsync(int TagId, bool includeProject)
        {
            throw new NotImplementedException();
        }
    }
}