using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface ICommentsPersistence
    {
        // COMMENTS
        Task<Comments[]> GetAllCommentsByCardIdAsync(string cardId);
        Task<Comments[]> GetAllCommentsAsync();
        Task<Comments> GetCommentByIdAsync(int CommentId);
    }
}