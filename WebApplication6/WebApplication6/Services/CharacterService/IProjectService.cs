using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Models;

namespace PracticeAPI.Services.ProjectService
{
    public interface IProjectService
    {
        Task<BaseResponse<Project>> Get(Guid id);
        Task<BaseResponse<Project>> GetAll();
        Task<BaseResponse<Project>> Post(CreateProjectRequest request);
        Task<BaseResponse<Project>> Put(Guid id, Project character);
        Task<BaseResponse<Project>> Delete(Guid id);
    }
}
