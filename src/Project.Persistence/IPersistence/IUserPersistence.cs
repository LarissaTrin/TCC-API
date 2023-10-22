using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Identity;

namespace Project.Persistence.IPersistence
{
    public interface IUserPersistence : IGeralPersistence
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}