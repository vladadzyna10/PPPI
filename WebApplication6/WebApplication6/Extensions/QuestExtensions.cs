using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;

namespace PracticeAPI.Extensions
{
    public static class QuestExtensions
    {
        public static Article ToModel(this CreateArticleRequest request)
        {
            return new Article
            {
                Name = request.Name,
                Description = request.Description,
            };
        }
    }
}
