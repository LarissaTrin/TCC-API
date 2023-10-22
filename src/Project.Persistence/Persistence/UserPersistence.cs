using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Identity;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;

namespace Project.Persistence.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersistence
    {
        private readonly DataContext _context;
        public UserPersistence(DataContext context) : base(context)
        {
            _context = context;
            
        }
        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}