using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class ProjectListDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public UserDTO? UserDTO { get; set; }
        public IEnumerable<ProjectUserDTO>? ProjectUsers { get; set; }
        public IEnumerable<ListDTO>? Lists { get; set; }
        public IEnumerable<TagsDTO>? Tags { get; set; }
    }
}