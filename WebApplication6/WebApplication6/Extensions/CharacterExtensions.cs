using PracticeAPI.DTO.Character;
using PracticeAPI.Models;

namespace PracticeAPI.Extensions
{
    public static class CharacterExtensions
    {
        public static Project ToModel(this CreateProjectRequest request)
        {
            return new Project
            {
                Title = request.Name,
                TeamSize = request.BaseATK,
                StartingBudget = request.BaseHP,
            };
        }
    }
}
