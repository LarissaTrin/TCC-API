using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class TaskPersistence : ITaskPersistence
    {
        public DataContext _context { get; }
        public TaskPersistence(DataContext context)
        {
            _context = context;
            
        }

        public async Task<TaskProject[]> GetAllTasksByCardIdAsync(int cardId)
        {
            IQueryable<TaskProject> query = _context.Tasks;

            query = query.AsNoTracking()
                         .Where(task => task.CardId == cardId);

            return await query.ToArrayAsync();
        }
    }
}