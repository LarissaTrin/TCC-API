using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class TaskProjectDTO
    {
        public int Id { get; set; }

        [Required]
        public string? TaskName { get; set; }
        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public string? Date { get; set; }
        public bool Completed { get; set; }
        public int CardId { get; set; }
    }
}