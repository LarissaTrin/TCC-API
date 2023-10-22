using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class ListDTO
    {
        public int Id { get; set; }

        [Required]
        public string? ListName { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public ProjectListDTO? Project { get; set; }
        public int Order { get; set; }
        public IEnumerable<CardDTO>? Cards { get; set; }
    }
}