using Microsoft.AspNetCore.Identity;

namespace Project.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles {get; set;}
    }
}