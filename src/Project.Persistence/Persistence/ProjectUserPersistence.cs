using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Identity;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;
using Project.Persistence.Models;

namespace Project.Persistence.Persistence
{
    public class ProjectUserPersistence : IProjectUserPersistence
    {
        public DataContext _context { get; }
        public ProjectUserPersistence(DataContext context)
        {
            _context = context;
        }
        public async Task<PageList<User>> GetAllUsersAsync(int userId, PageParams pageParams)
        {
            IQueryable<User> query = _context.Users;
                
            query = query.AsNoTracking()
                         .Where(u => (u.FirstName.ToLower().Contains(pageParams.Term.ToLower()) ||
                                u.LastName.ToLower().Contains(pageParams.Term.ToLower()) ||
                                u.Email.ToLower().Contains(pageParams.Term.ToLower()) ||
                                u.UserName.ToLower().Contains(pageParams.Term.ToLower())) &&
                                u.Id != userId);

            query = query.OrderBy(u => u.FirstName);

            return await PageList<User>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }
        public async Task<PageList<User>> GetAllUsersByEditAsync(int projectId, PageParams pageParams)
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                .Where(u => u.FirstName.ToLower().Contains(pageParams.Term.ToLower()) ||
                            u.LastName.ToLower().Contains(pageParams.Term.ToLower()) ||
                            u.Email.ToLower().Contains(pageParams.Term.ToLower()) ||
                            u.UserName.ToLower().Contains(pageParams.Term.ToLower()));

            var subquery = _context.ProjectUsers.Where(u => u.ProjectId == projectId);

            query = query.Where(u => !subquery.Select(s => s.UserId).Contains(u.Id));

            query = query.OrderBy(u => u.FirstName);

            return await PageList<User>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }
    }
}