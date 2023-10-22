namespace Project.Domain
{
    public class TagCard
    {
        public int CardId { get; set; }
        public Card? Card { get; set; }
        public int TagId { get; set; }
        public IEnumerable<Tags>? Tags { get; set; }
    }
}