namespace Project.Application.DTOs
{
    public class ProjectUserDTO
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public int? Order { get; set; }
        public UserDTO? User { get; set; }
        public ProjectListDTO? Project { get; set; }
    }
}