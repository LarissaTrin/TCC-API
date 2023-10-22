using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Services;
using Project.Domain;
using Project.Persistence.IPersistence;

namespace Project.Application
{
    public class CardService : ICardService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly ICardPersistence _cardPersistence;
        public readonly IMapper _mapper;
        public CardService(IGeralPersistence geralPersistence,
                           ICardPersistence cardPersistence,
                           IMapper mapper)
        {
            _cardPersistence = cardPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }
        public async Task<CardDTO> AddCard(int ListId, CardDTO model)
        {
            try
            {
                var card = _mapper.Map<Card>(model);
                card.ListId = ListId;

                _geralPersistence.Add(card);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var result = await _cardPersistence.GetCardByIdAsync(card.Id);
                    return _mapper.Map<CardDTO>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CardDTO> UpdateCard(int cardId, CardDTO model)
        {
            try
            {
                var card = await _cardPersistence.GetCardByIdAsync(cardId);
                if (card == null) return null;

                if (model.Approvers != null){
                    var approversList = model.Approvers;

                    foreach (var approver in card.Approvers)
                    {
                        var list = approversList.FirstOrDefault(list => list.Id == approver.Id);
                        if (list == null)
                        {
                            _geralPersistence.Delete<Approver>(approver);
                            await _geralPersistence.SaveChangesAsync();
                        }
                    }
                }

                if (model.TasksProject != null){
                    var tasksList = model.TasksProject;
                    foreach (var task in card.TasksProject)
                    {
                        var list = tasksList.FirstOrDefault(list => list.Id == task.Id);
                        if (list == null)
                        {
                            _geralPersistence.Delete<TaskProject>(task);
                            await _geralPersistence.SaveChangesAsync();
                        }
                    }
                }

                model.Id = cardId;
                _mapper.Map(model, card);

                _geralPersistence.Update(card);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var result = await _cardPersistence.GetCardByIdAsync(cardId);
                    return _mapper.Map<CardDTO>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCard(int ListId, int cardId)
        {
            try
            {
                var card = await _cardPersistence.GetCardByIdAsync(cardId);
                if (card == null) throw new Exception("Don't found Id");

                _geralPersistence.Delete<Card>(card);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CardDTO[]> GetAllCardsByListAsync(int ListId)
        {
            try
            {
                var cards = await _cardPersistence.GetAllCardsByListAsync(ListId);
                if (cards == null) return null;

                var result = _mapper.Map<CardDTO[]>(cards);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CardDTO> GetCardByIdAsync(int cardId)
        {
            try
            {
                var list = await _cardPersistence.GetCardByIdAsync(cardId);
                if (list == null) return null;

                var result = _mapper.Map<CardDTO>(list);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}