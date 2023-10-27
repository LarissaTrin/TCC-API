using Project.Domain.Identity;

namespace Project.Domain
{
    public class TaskProject
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? Date { get; set; }
        public bool Completed { get; set; }
        public int CardId { get; set; }
    }
}