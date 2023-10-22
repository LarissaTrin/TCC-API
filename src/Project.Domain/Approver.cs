using Project.Domain.Identity;

namespace Project.Domain
{
    public class Approver
    {
        public int Id { get; set; }
        public string? Environment { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int CardId { get; set; }
    }
}