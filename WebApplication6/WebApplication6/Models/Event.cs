namespace PracticeAPI.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now;
        public int EventType { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
