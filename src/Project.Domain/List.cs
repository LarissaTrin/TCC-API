namespace Project.Domain
{
    public class List
    {
        public int Id { get; set; }
        public string? ListName { get; set; }
        public int ProjectId { get; set; }
        public ProjectList? Project { get; set; }
        public int Order { get; set; }
        public IEnumerable<Card>? Cards { get; set; }
    }
}