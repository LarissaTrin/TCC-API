using Project.Domain.Identity;

namespace Project.Domain
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public int CardId { get; set; }
    }
}