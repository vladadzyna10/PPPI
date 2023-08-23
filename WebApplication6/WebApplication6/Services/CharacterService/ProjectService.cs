using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly List<Project> _projectRepository;

        public ProjectService()
        {
            _projectRepository = new List<Project>()
            {
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Website Redesign",
                    StartingBudget = 5000,
                    TeamSize = 10,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Mobile App Development",
                    StartingBudget = 8000,
                    TeamSize = 15,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "E-commerce Platform",
                    StartingBudget = 10000,
                    TeamSize = 20,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Marketing Campaign",
                    StartingBudget = 3000,
                    TeamSize = 5,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Product Launch",
                    StartingBudget = 12000,
                    TeamSize = 25,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Software Upgrade",
                    StartingBudget = 6000,
                    TeamSize = 12,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Event Planning",
                    StartingBudget = 4000,
                    TeamSize = 8,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Social Media Strategy",
                    StartingBudget = 2000,
                    TeamSize = 6,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Content Creation",
                    StartingBudget = 2500,
                    TeamSize = 7,
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Video Production",
                    StartingBudget = 7000,
                    TeamSize = 18,
                }
            };
        }

        public async Task<BaseResponse<Project>> Get(Guid id)
        {
            try
            {
                var project = await Task.FromResult(_projectRepository.Find(g => g.Id == id));
                if (project != null)
                {
                    return new BaseResponse<Project>()
                    {
                        Success = true,
                        Values = new List<Project> { project },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<Project>
                {
                    Message = "Project not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Project>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Project>> GetAll()
        {
            try
            {
                var projects = await Task.FromResult(_projectRepository);
                return new BaseResponse<Project>()
                {
                    Success = true,
                    Values = projects,
                    ValueCount = projects.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Project>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Project>> Post(CreateProjectRequest request)
        {
            try
            {
                var project = request.ToModel();
                var id = await Task.FromResult(project.Id = Guid.NewGuid());
                _projectRepository.Add(project);

                return new BaseResponse<Project>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<Project> { project },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Project>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Project>> Put(Guid id, Project project)
        {
            try
            {
                if (project.Id != id)
                {
                    return new BaseResponse<Project>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_projectRepository.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<Project>()
                    {
                        Message = "Project not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(project.Title))
                {
                    current.Title = project.Title;
                }
                if (project.StartingBudget >= 0)
                {
                    current.StartingBudget = project.StartingBudget;
                }
                if (project.TeamSize >= 0)
                {
                    current.TeamSize = project.TeamSize;
                }

                return new BaseResponse<Project>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<Project> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Project>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Project>> Delete(Guid id)
        {
            try
            {
                var current = _projectRepository.Find(x => x.Id == id);
                if (current == null)
                {
                    return new BaseResponse<Project>()
                    {
                        Message = "Project not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_projectRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<Project>()
                    {
                        Success = true,
                        Message = "Project deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<Project>()
                {
                    Message = "The project was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Project>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
