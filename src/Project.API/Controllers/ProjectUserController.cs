using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Application.DTOs;
using Project.API.Extensions;
using Project.Persistence.Models;

namespace Project.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectUserController : ControllerBase
{
    private readonly IProjectUserService _projectUserService;
    public ProjectUserController(IProjectUserService projectUserService)
    {
        _projectUserService = projectUserService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery]PageParams pageParams)
    {
        try
        {
            var projectUsers = await _projectUserService.GetAllUsersAsync(User.GetUserId(), pageParams);
            if (projectUsers == null) return NoContent();

            return Ok(projectUsers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetAllByEditAsync(int projectId, [FromQuery]PageParams pageParams)
    {
        try
        {
            var projectUsers = await _projectUserService.GetAllUsersByEditAsync(projectId, pageParams);
            if (projectUsers == null) return BadRequest();

            return Ok(projectUsers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> Put(int projectId, ProjectUserDTO[] models)
    {
        try
        {
            var lists = await _projectUserService.SaveUsersByProject(projectId, models);
            if (lists == null) return NoContent();

            return Ok(lists);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }
}
