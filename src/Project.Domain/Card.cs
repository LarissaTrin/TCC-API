using Project.Domain.Identity;

namespace Project.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public required string CardName { get; set; }
        public int ListId { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? Date { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }
        public string? PlannedHours { get; set; }
        public string? CompletedHours { get; set; }
        public int? StoryPoints { get; set; }
        public IEnumerable<TagCard>? TagCards { get; set; }
        public IEnumerable<Comments>? Comments { get; set; }
        public IEnumerable<Approver>? Approvers { get; set; }
        public IEnumerable<TaskProject>? TasksProject { get; set; }
    }
}