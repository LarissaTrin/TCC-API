using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class CardPersistence : ICardPersistence
    {
        public DataContext _context { get; }
        public CardPersistence(DataContext context)
        {
            _context = context;
            
        }

        public async Task<Card[]> GetAllCardsByListAsync(int ListId)
        {
            IQueryable<Card> query = _context.Cards
                .Include(c => c.User)
                .Include(c => c.Approvers).ThenInclude(a => a.User)
                .Include(c => c.TasksProject).ThenInclude(t => t.User);

            query = query.AsNoTracking()
                         .Where(card => card.ListId == ListId);

            return await query.ToArrayAsync();
        }

        public async Task<Card> GetCardByIdAsync(int cardId)
        {
            IQueryable<Card> query = _context.Cards
                .Include(c => c.User)
                .Include(c => c.Approvers).ThenInclude(a => a.User)
                .Include(c => c.TasksProject).ThenInclude(t => t.User);

            query = query.AsNoTracking()
                         .Where(card => card.Id == cardId);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}