using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class CommentsDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Date { get; set; }
        public int CardId { get; set; }
    }
}