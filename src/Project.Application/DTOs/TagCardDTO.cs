namespace Project.Application.DTOs
{
    public class TagCardDTO
    {
        public int CardId { get; set; }
        public CardDTO? Card { get; set; }
        public int TagId { get; set; }
        public IEnumerable<TagsDTO>? Tags { get; set; }
    }
}