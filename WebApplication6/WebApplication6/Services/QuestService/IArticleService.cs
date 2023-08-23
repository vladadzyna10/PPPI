using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;

namespace PracticeAPI.Services.ArticleService
{
    public interface IArticleService
    {
        Task<BaseResponse<Article>> Get(Guid id);
        Task<BaseResponse<Article>> GetAll();
        Task<BaseResponse<Article>> Post(CreateArticleRequest request);
        Task<BaseResponse<Article>> Put(Guid id, Article article);
        Task<BaseResponse<Article>> Delete(Guid id);
    }
}
