using Project.Domain;

namespace Project.Persistence.IPersistence
{
    public interface ICardPersistence
    {
        // CARDS
        Task<Card[]> GetAllCardsByListAsync(int ListId);
        Task<Card> GetCardByIdAsync(int cardId);
    }
}