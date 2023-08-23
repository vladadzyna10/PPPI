namespace PracticeAPI.DTO.GameAccount
{
    public class UpdateEventRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int EventType { get; set; }
        public IEnumerable<Guid> ProjectIds { get; set; } = new HashSet<Guid>();
        public IEnumerable<Guid> ArticleIds { get; set; } = new HashSet<Guid>();
    }
}
