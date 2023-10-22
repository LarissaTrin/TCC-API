using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class ApproverPersistence : IApproverPersistence
    {
        public DataContext _context { get; }
        public ApproverPersistence(DataContext context)
        {
            _context = context;
            
        }

        public async Task<Approver[]> GetAllApproverByCardIdAsync(int cardId)
        {
            IQueryable<Approver> query = _context.Approvers;

            query = query.AsNoTracking()
                         .Where(task => task.CardId == cardId);

            return await query.ToArrayAsync();
        }
    }
}