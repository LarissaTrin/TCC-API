using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Application.DTOs;

namespace Project.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListController : ControllerBase
{
    private readonly IListService _listService;
    public ListController(IListService listService)
    {
        _listService = listService;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> Get(int projectId)
    {
        try
        {
            var lists = await _listService.GetListsByProjectIdAsync(projectId);
            if (lists == null) return NoContent();

            return Ok(lists);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> Put(int projectId, ListDTO[] models)
    {
        try
        {
            var lists = await _listService.SaveLists(projectId, models);
            if (lists == null) return NoContent();

            return Ok(lists);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpDelete("{projectId}/{listId}")]
    public async Task<IActionResult> Delete(int projectId, int listId)
    {
        try
        {
            var list = await _listService.GetListByIdsAsync(projectId, listId);
            if (list == null) return NoContent();

            return await _listService.DeleteList(list.ProjectId, listId) ? 
                   Ok(new {message = "Deleted"}) :
                   throw new Exception("Error deleted List");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }
}
