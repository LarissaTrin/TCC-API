using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence
{
    public class ProjectListPersistence : IProjectListPersistence
    {
        public DataContext _context { get; }
        public ProjectListPersistence(DataContext context)
        {
            _context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // PROJECTS
        public Task<ProjectList[]> GetAllProjectsByUserAsync(int userId, string user, bool includeUsers, bool includeTags, bool includeLists)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectList[]> GetAllProjectsAsync(int userId)
        {
            IQueryable<ProjectList> query = _context.Projects
                .Include(p => p.Lists)
                .Include(p => p.ProjectUsers);

            var subquery = _context.ProjectUsers.Where(pu => pu.UserId == userId);

            query = query.Where(p => subquery.Any(s => s.ProjectId == p.Id));

            query = query.OrderBy(p => p.Id);

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<ProjectList> GetProjectByIdAsync(int userId, int projectId)
        {
            IQueryable<ProjectList> query = _context.Projects
                .Include(p => p.Tags)
                .Include(p => p.Lists)
                .Include(p => p.ProjectUsers).ThenInclude(pu => pu.User);

            var subquery = _context.ProjectUsers.Where(pu => pu.UserId == userId);

            query = query.Where(p => subquery.Any(s => s.ProjectId == p.Id));
                
            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id == projectId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ProjectUser> GetUserByIdByProjectAsync(int projectId, int userId)
        {
            IQueryable<ProjectUser> query = _context.ProjectUsers;
                
            query = query.AsNoTracking().Where(p => p.ProjectId == projectId && p.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ProjectUser[]> GetUsersByProjectByIdAsync(int projectId)
        {
            IQueryable<ProjectUser> query = _context.ProjectUsers;
                
            query = query.AsNoTracking().Where(p => p.ProjectId == projectId);

            return await query.ToArrayAsync();
        }
    }
}