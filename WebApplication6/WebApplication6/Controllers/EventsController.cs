using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO;
using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;
using PracticeAPI.Services.GameAccountService;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _gameAccountService;

        public EventsController(IEventService gameAccountService)
        {
            _gameAccountService = gameAccountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Event>>> Get(Guid id)
        {
            var gameAccount = await _gameAccountService.Get(id);
            if (gameAccount == null || gameAccount.ValueCount == 0)
            {
                return NotFound(gameAccount);
            }

            return Ok(gameAccount);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Event>>> GetAll()
        {
            var gameAccounts = await _gameAccountService.GetAll();
            return Ok(gameAccounts);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Event>>> Post([FromBody] CreateEventRequest request)
        {
            var gameAccount = await _gameAccountService.Post(request);
            return Ok(gameAccount);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Event>>> Put(Guid id, [FromBody] UpdateEventRequest ugar)
        {
            var response = await _gameAccountService.Put(id, ugar);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Event>>> Delete(Guid id)
        {
            await _gameAccountService.Delete(id);
            return NoContent();
        }
    }
}
