namespace PracticeAPI.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int StartingBudget { get; set; } = 10000;
        public int TeamSize { get; set; } = 5;
    }
}
