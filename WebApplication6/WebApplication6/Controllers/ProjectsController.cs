using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Models;
using PracticeAPI.Services.ProjectService;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Project>>> Get(Guid id)
        {
            var project = await _projectService.Get(id);
            if (project == null || project.ValueCount == 0)
            {
                return NotFound(project);
            }

            return Ok(project);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Project>>> GetAll()
        {
            return Ok(await _projectService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Project>>> Post([FromBody] CreateProjectRequest request)
        {
            var project = await _projectService.Post(request);
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Project>>> Put(Guid id, [FromBody] Project project)
        {
            var response = await _projectService.Put(id, project);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Project>>> Delete(Guid id)
        {
            await _projectService.Delete(id);
            return NoContent();
        }
    }
}
