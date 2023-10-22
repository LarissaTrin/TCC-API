namespace Project.Domain
{
    public class Tags
    {
        public int Id { get; set; }
        public string? TagName { get; set; }
        public int ProjectId { get; set; }
        public ProjectList? Project { get; set; }
    }
}