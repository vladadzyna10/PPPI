using PracticeAPI.DTO;
using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;

namespace PracticeAPI.Services.GameAccountService
{
    public interface IEventService
    {
        Task<BaseResponse<Event>> Get(Guid id);
        Task<BaseResponse<Event>> GetAll();
        Task<BaseResponse<Event>> Post(CreateEventRequest request);
        Task<BaseResponse<Event>> Put(Guid id, UpdateEventRequest ugar);
        Task<BaseResponse<Event>> Delete(Guid id);
    }
}
