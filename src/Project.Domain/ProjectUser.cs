using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Identity;

namespace Project.Domain
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public User? User { get; set; }
        public ProjectList? Project { get; set; }
    }
}