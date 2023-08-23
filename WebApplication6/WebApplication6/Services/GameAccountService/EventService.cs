using PracticeAPI.DTO;
using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;
using PracticeAPI.Services.ProjectService;
using PracticeAPI.Services.ArticleService;
using System.Linq;

namespace PracticeAPI.Services.GameAccountService
{
    public class EventService : IEventService
    {
        private readonly IProjectService _projectService;
        private readonly IArticleService _articleService;
        private readonly List<Event> _Events = new List<Event>();

        public EventService(IProjectService projectService, IArticleService articleService)
        {
            _projectService = projectService;
            _articleService = articleService;

            InitEvents();
        }

        private async void InitEvents()
        {
            var projects = await _projectService.GetAll();
            var articles = await _articleService.GetAll();
            var random = new Random();

            // Create 10 game accounts
            for (int i = 0; i < 10; i++)
            {
                var @event = new Event
                {
                    Id = Guid.NewGuid(),
                    Name = $"Event{i + 1}",
                    EventDate = DateTime.Now,
                    EventType = 1,
                };

                // Assign random characters to the game account
                for (int j = 0; j < 3; j++)
                {
                    var project = projects.Values.ElementAt(random.Next(projects.ValueCount));
                    @event.Projects.Add(project);
                }

                // Assign random quests to the game account
                for (int j = 0; j < 3; j++)
                {
                    var article = articles.Values.ElementAt(random.Next(projects.ValueCount-1));
                    @event.Articles.Add(article);
                }

                _Events.Add(@event);
            }
        }

        public async Task<BaseResponse<Event>> Get(Guid id)
        {
            try
            {
                var @event = await Task.FromResult(_Events.Find(x => x.Id == id));
                if (@event != null)
                {
                    return new BaseResponse<Event>()
                    {
                        Success = true,
                        Values = new List<Event> { @event },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<Event>()
                {
                    Message = "Event not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Event>> GetAll()
        {
            try
            {
                var @event = await Task.FromResult(_Events);
                return new BaseResponse<Event>()
                {
                    Success = true,
                    Values = @event,
                    ValueCount = @event.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Event>> Post(CreateEventRequest request)
        {
            try
            {
                var projects = await Task.WhenAll(request.ProjectIds.Select(_projectService.Get));
                var articles = await Task.WhenAll(request.ArticleIds.Select(_articleService.Get));

                var @event = new Event
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    EventDate = DateTime.Now,
                    EventType = 1,

                    Projects = projects.Select(x => x.Values.First()).ToList(),
                    Articles = articles.Select(x => x.Values.First()).ToList(),
                };

                _Events.Add(@event);

                return new BaseResponse<Event>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<Event> { @event },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Event>> Put(Guid id, UpdateEventRequest request)
        {
            try
            {
                if (request.Id != id)
                {
                    return new BaseResponse<Event>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_Events.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<Event>()
                    {
                        Message = "Event not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    current.Name = request.Name;
                }
                current.EventType = request.EventType;

                var projects = await Task.WhenAll(request.ProjectIds.Select(_projectService.Get));
                var articles = await Task.WhenAll(request.ArticleIds.Select(_articleService.Get));

                current.Projects = projects.Select(x => x.Values.First()).ToList();
                current.Articles = articles.Select(x => x.Values.First()).ToList();

                return new BaseResponse<Event>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<Event> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<BaseResponse<Event>> Delete(Guid id)
        {
            try
            {
                var current = await Task.FromResult(_Events.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<Event>()
                    {
                        Message = "Event not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _Events.Remove(current);

                return new BaseResponse<Event>()
                {
                    Success = true,
                    Message = "Event deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
