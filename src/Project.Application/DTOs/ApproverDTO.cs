using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class ApproverDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Environment { get; set; }
        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public int CardId { get; set; }
    }
}