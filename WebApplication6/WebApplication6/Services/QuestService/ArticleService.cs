using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly List<Article> _articlesRepository;

        public ArticleService()
        {
            _articlesRepository = new List<Article>()
            {
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Uncover the Enchanted Scroll",
                    Description = "The Enchanted Scroll is said to contain ancient wisdom and magic that can only be deciphered by the pure of heart. It is hidden in a forgotten library, guarded by puzzles and riddles.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Journey to the Starlit Cavern",
                    Description = "The Starlit Cavern is a mystical place where the walls are adorned with glowing crystals that light up the darkness. Legend has it that anyone who reaches the heart of the cavern gains insights into the future.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Conquer the Labyrinth of Shadows",
                    Description = "The Labyrinth of Shadows is a maze filled with illusions and traps that challenge even the bravest. At its center lies a treasure said to grant the power of seeing through deception.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Discover the Secrets of the Celestial Observatory",
                    Description = "The Celestial Observatory is a place of astronomical wonder, where the skies reveal ancient prophecies. Seekers who decipher the celestial patterns may gain insight into the mysteries of the universe.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Defeat the Guardian of the Whispering Woods",
                    Description = "The Whispering Woods are inhabited by an ethereal guardian who protects the secrets of nature. Only those who prove their respect for the environment are granted passage to the heart of the woods.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Unlock the Vault of Eternity",
                    Description = "The Vault of Eternity is a place beyond time, where wisdom and artifacts from different ages are stored. Those who unlock its doors may gain the ability to traverse history and learn from the past.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Retrieve the Crystal of Serenity",
                    Description = "The Crystal of Serenity is said to bring peace and tranquility to its bearer. It's guarded by a guardian who tests the seeker's inner harmony before granting access to the crystal's resting place.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Solve the Riddle of the Whispering Sphinx",
                    Description = "The Whispering Sphinx guards a riddle that challenges the intellect of all who approach. Solving the riddle not only grants passage but also the Sphinx's favor and guidance.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Harvest the Fields of Unity",
                    Description = "The Fields of Unity are a mystical landscape where harmony is key. Those who tend to the fields with empathy and kindness can harvest fruits that are said to heal divisions and mend relationships.",
                },
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Name = "Awaken the Beacon of Hope",
                    Description = "The Beacon of Hope is a hidden light that shines in times of despair. Its flame is ignited by acts of compassion and bravery, and those who awaken it bring positivity to the world.",
                },
            };
        }


        public async Task<BaseResponse<Article>> Get(Guid id)
        {
            try
            {
                var article = await Task.FromResult(_articlesRepository.Find(g => g.Id == id));
                if (article != null)
                {
                    return new BaseResponse<Article>()
                    {
                        Success = true,
                        Values = new List<Article> { article },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<Article>
                {
                    Message = "Article not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Article>> GetAll()
        {
            try
            {
                var quests = await Task.FromResult(_articlesRepository);
                return new BaseResponse<Article>()
                {
                    Success = true,
                    Values = quests,
                    ValueCount = quests.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Article>> Post(CreateArticleRequest request)
        {
            try
            {
                var article = request.ToModel();
                var id = await Task.FromResult(() => {
                    article.Id = Guid.NewGuid();
                    _articlesRepository.Add(article);
                    return article.Id;
                });

                return new BaseResponse<Article>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<Article> { article },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Article>> Put(Guid id, Article article)
        {
            try
            {
                if (article.Id != id)
                {
                    return new BaseResponse<Article>() { 
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_articlesRepository.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<Article>()
                    {
                        Message = "Article not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(article.Name))
                {
                    current.Name = article.Name;
                }
                if (!string.IsNullOrWhiteSpace(article.Description))
                {
                    current.Description = article.Description;
                }

                return new BaseResponse<Article>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<Article> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Article>> Delete(Guid id)
        {
            try
            {
                var current = _articlesRepository.Find(x => x.Id == id);
                if (current == null)
                {
                    return new BaseResponse<Article>()
                    {
                        Message = "Article not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_articlesRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<Article>()
                    {
                        Success = true,
                        Message = "Article deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<Article>()
                {
                    Message = "The article was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
