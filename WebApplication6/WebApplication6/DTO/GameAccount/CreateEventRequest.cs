namespace PracticeAPI.DTO.GameAccount
{
    public class CreateEventRequest
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Guid> ProjectIds { get; set; } = new HashSet<Guid>();
        public IEnumerable<Guid> ArticleIds { get; set; } = new HashSet<Guid>();
    }
}
