using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class CardDTO
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }

        [Required]
        public required string CardName { get; set; }
        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public int ListId { get; set; }
        public string? Date { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }
        public string? PlannedHours { get; set; }
        public string? CompletedHours { get; set; }
        public int? StoryPoints { get; set; }
        public IEnumerable<TagCardDTO>? TagCards { get; set; }
        public IEnumerable<CommentsDTO>? Comments { get; set; }
        public IEnumerable<ApproverDTO>? Approvers { get; set; }
        public IEnumerable<TaskProjectDTO>? TasksProject { get; set; }
    }
}