using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Application.DTOs;
using Project.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Project.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectListService _projectListService;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IAccountService _accountService;
    public ProjectController(IProjectListService projectListService,
                            IHostEnvironment hostEnvironment,
                            IAccountService accountService)
    {
        _projectListService = projectListService;
        _hostEnvironment = hostEnvironment;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var projects = await _projectListService.GetAllProjectsAsync(User.GetUserId());
            if (projects == null) return NoContent();

            return Ok(projects);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var project = await _projectListService.GetProjectByIdAsync(User.GetUserId(), id);
            if (project == null) return BadRequest();

            return Ok(project);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(ProjectListDTO model)
    {
        try
        {
            var project = await _projectListService.AddProject(User.GetUserId(), model);
            if (project == null) return NoContent();

            return Ok(project);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProjectListDTO model)
    {
        try
        {
            var project = await _projectListService.UpdateProject(User.GetUserId(), id, model);
            if (project == null) return NoContent();

            return Ok(project);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var project = await _projectListService.GetProjectByIdAsync(User.GetUserId(), id);
            if (project == null) return NoContent();

            return await _projectListService.DeleteProject(User.GetUserId(), id) ? 
                   Ok(new {message = "Deleted"}) :
                   throw new Exception("Error deleted Project");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpPut("User/{projectId}")]
    public async Task<IActionResult> UpdateUsersByProject(int projectId, ProjectUserDTO[] projectUsers)
    {
        try
        {
            var listsUsers = await _projectListService.SaveUsersByProject(projectId, projectUsers);
            if (listsUsers == null) return NoContent();

            return Ok(listsUsers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }
}
