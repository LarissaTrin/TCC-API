using Project.Application.DTOs;

namespace Project.Application.Services
{
    public interface ICardService
    {
        Task<CardDTO> AddCard(int ListId, CardDTO model);
        Task<CardDTO> UpdateCard(int cardId, CardDTO model);
        Task<bool> DeleteCard(int ListId, int cardId);

        Task<CardDTO[]> GetAllCardsByListAsync(int projectId);
        Task<CardDTO> GetCardByIdAsync(int cardId);
    }
}