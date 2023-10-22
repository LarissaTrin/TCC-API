using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class ListPersistence : IListPersistence
    {
        public DataContext _context { get; }
        public ListPersistence(DataContext context)
        {
            _context = context;
            
        }

        public async Task<List[]> GetListsByProjectIdAsync(int projectId)
        {
            IQueryable<List> query = _context.Lists
                                    .Include(ev => ev.Cards);

            query = query.AsNoTracking()
                         .Where(list => list.ProjectId == projectId);

            return await query.ToArrayAsync();
        }

        public async Task<List> GetListByIdsAsync(int projectId, int ListId)
        {
            IQueryable<List> query = _context.Lists
                                    .Include(ev => ev.Cards);

            query = query.AsNoTracking()
                         .Where(list => list.ProjectId == projectId
                                        && list.Id == ListId);
            return await query.FirstOrDefaultAsync();
        }
    }
}