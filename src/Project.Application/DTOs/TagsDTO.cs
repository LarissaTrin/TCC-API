using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class TagsDTO
    {
        public int Id { get; set; }

        [Required]
        public string? TagName { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public ProjectListDTO? Project { get; set; }
    }
}