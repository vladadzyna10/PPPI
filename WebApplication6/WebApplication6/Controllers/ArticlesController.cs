using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;
using PracticeAPI.Services.ArticleService;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _questService;

        public ArticlesController(IArticleService questService)
        {
            _questService = questService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Article>>> Get(Guid id)
        {
            var quest = await _questService.Get(id);
            if (quest == null || quest.ValueCount == 0)
            {
                return NotFound(quest);
            }

            return Ok(quest);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Article>>> GetAll()
        {
            var quests = await _questService.GetAll();
            return Ok(quests);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Article>>> Post([FromBody] CreateArticleRequest request)
        {
            var quest = await _questService.Post(request);
            return Ok(quest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Article>>> Put(Guid id, [FromBody] Article quest)
        {
            var response = await _questService.Put(id, quest);

            if (response == null || !response.Success) {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Article>>> Delete(Guid id)
        {
            await _questService.Delete(id);
            return NoContent();
        }
    }
}
