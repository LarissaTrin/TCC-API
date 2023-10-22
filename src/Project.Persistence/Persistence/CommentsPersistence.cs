using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class CommentsPersistence : ICommentsPersistence
    {
        public DataContext _context { get; }
        public CommentsPersistence(DataContext context)
        {
            _context = context;
            
        }

        public Task<Comments[]> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comments[]> GetAllCommentsByCardIdAsync(string cardId)
        {
            throw new NotImplementedException();
        }

        public Task<Comments> GetCommentByIdAsync(int CommentId)
        {
            throw new NotImplementedException();
        }
    }
}