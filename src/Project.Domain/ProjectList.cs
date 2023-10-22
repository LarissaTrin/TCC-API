using Project.Domain.Identity;

namespace Project.Domain
{
    public class ProjectList
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public IEnumerable<List>? Lists { get; set; }
        public IEnumerable<ProjectUser>? ProjectUsers { get; set; }
        public IEnumerable<Tags>? Tags { get; set; }
    }
}