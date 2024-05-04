using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using YuTechsBL.Const;
using YuTechsBL.Dtos;
using YuTechsBL.UnitOfWork;
using YuTechsEF.Entites;
using YuTechsEF.Entity;


namespace YuTechsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unitOfWork.News.GetAllAsync(new string[] { "Author" });
            if (items != null)
            {
                var newsData = items.Select(i => new NewsDto { Id = i.Id, CreationDate = i.CreationDate, Description = i.Description,
                    ImageUrl = i.ImageUrl, Title = i.Title, NewsContent = i.NewsContent, PublicationDate = i.PublicationDate,
                    Author = new AuthorDto {AuthorName=i.Author.AuthorName,Country=i.Author.Country,Biography=i.Author.Biography,AuthorId=i.AuthorId},
                });
                return Ok(newsData);

            }else  return BadRequest("No Data Found"); 

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _unitOfWork.News.GetByIdAsync(i => i.Id == id, new string[] { "Author" });
            if (item != null)
            {
                var newsData = new NewsDto
                {
                    Id = item.Id,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Title = item.Title,
                    NewsContent = item.NewsContent,
                    PublicationDate = item.PublicationDate,
                    Author = new AuthorDto
                    {
                        AuthorName = item.Author.AuthorName,
                        Country = item.Author.Country,
                        Biography = item.Author.Biography,
                        AuthorId = item.AuthorId
                        
                    }
                };
                return Ok(newsData);
            }
            else
            {
                return NotFound("News not found");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.AuthorId == 0)
                {
                    return BadRequest("AuthorId is required.");
                }

                await _unitOfWork.News.AddedAsync(news);
                return CreatedAtAction(nameof(GetById), new { id = news.Id }, news);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByname(string name)
        {
            var item = await _unitOfWork.News.GetByIdAsync(i => i.Title.Contains(name), new string[] { "Author" });
            if (item != null)
            {
                var newsData = new NewsDto
                {
                    Id = item.Id,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Title = item.Title,
                    NewsContent = item.NewsContent,
                    PublicationDate = item.PublicationDate,
                    Author = new AuthorDto
                    {
                        AuthorName = item.Author.AuthorName,
                        Country = item.Author.Country,
                        Biography = item.Author.Biography
                    }
                };
                return Ok(newsData);
            }
            else return BadRequest(" Not Found News with name:" + name );
        }

        [HttpPut]
        public async Task<IActionResult> Update( News news)
        {
            if (news.Id == 0)
                return BadRequest("Invalid Author Id"); 
            
            if (ModelState.IsValid)
            {
                await _unitOfWork.News.UpdateAsync(i => true,news);
                return Ok(news);
            }
            else return BadRequest(ModelState);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("News not Found");
            }
            else 
            {
                await _unitOfWork.News.DeleteByIdAsync(i=>i.Id==id);
                return Ok("News With id: " + id + " deleted Successfully");
            
            }    
        }


        [HttpGet("GetNewsOrderBy")]
        public async Task<IActionResult> GetNews(string newsNameFilter = null, string orderBy = "Id", string orderByDirection = "ASC")
        {
            Expression<Func<News, bool>> filter=null;
            if (!string.IsNullOrEmpty(newsNameFilter)) 
            {
              filter=i=>i.Title.Contains(newsNameFilter);
            }

            Expression<Func<News, object>> orderByExpression = null;
            switch (orderBy.ToLower())
            {
                case "title":
                    orderByExpression = news => news.Title;
                    break;
                case "description":
                    orderByExpression = news => news.Description;
                    break;
                case "creationdate":
                    orderByExpression = news => news.CreationDate;
                    break;
                default:
                    orderByExpression = news => news.Id;
                    break;
            }

            var items = await _unitOfWork.News.OrderItems(filter,orderByExpression ,orderByDirection,new string[] {"Author"});


            if (items != null)
            {
                var newsData = items.Select(i => new NewsDto
                {
                    Id = i.Id,
                    CreationDate = i.CreationDate,
                    Description = i.Description,
                    ImageUrl = i.ImageUrl,
                    Title = i.Title,
                    NewsContent = i.NewsContent,
                    PublicationDate = i.PublicationDate,
                    Author = new AuthorDto { AuthorName = i.Author.AuthorName, Country = i.Author.Country, Biography = i.Author.Biography, AuthorId = i.AuthorId },
                });
                return Ok(newsData);

            }
            else return BadRequest("No Data Found");

        }


    }
}

