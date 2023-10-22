using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Application.DTOs;

namespace Project.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("{ListId}")]
    public async Task<IActionResult> Get(int ListId)
    {
        try
        {
            var cards = await _cardService.GetAllCardsByListAsync(ListId);
            if (cards == null) return NoContent();

            return Ok(cards);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpGet("{ListId}/{cardId}")]
    public async Task<IActionResult> GetByIdAsync(int ListId, int cardId)
    {
        try
        {
            var cards = await _cardService.GetCardByIdAsync(cardId);
            if (cards == null) return NoContent();

            return Ok(cards);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPost("{ListId}")]
    public async Task<IActionResult> PostAsync(int ListId, CardDTO model)
    {
        try
        {
            var card = await _cardService.AddCard(ListId, model);
            if (card == null) return NoContent();

            return Ok(card);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CardDTO model)
    {
        try
        {
            var card = await _cardService.UpdateCard(id, model);
            if (card == null) return NoContent();

            return Ok(card);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpDelete("{ListId}/{cardId}")]
    public async Task<IActionResult> Delete(int ListId, int cardId)
    {
        try
        {
            var card = await _cardService.GetCardByIdAsync(cardId);
            if (card == null) return NoContent();

            return await _cardService.DeleteCard(ListId, cardId) ? 
                   Ok(new {message = "Deleted"}) :
                   throw new Exception("Error deleted Card");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }
}
