using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using YuTechsBL.Const;
using YuTechsBL.Dtos;
using YuTechsBL.UnitOfWork;
using YuTechsEF.Entity;


namespace YuTechsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork= unitOfWork;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unitOfWork.Authors.GetAllAsync(new string[] { "News" });
            if (items != null)
            {
                var authorsData = items.Select(i => new AuthWithNewsDto
                {
                    AuthorName = i.AuthorName,
                    Id = i.Id,
                    Country = i.Country,
                    Biography=i.Biography,
                    
                    RelatNews = i.News.Select(i => new AuthRelatNewsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                        NewsContent = i.NewsContent,
                        CreationDate = i.CreationDate,
                        PublicationDate = i.PublicationDate,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                }) ;
                return Ok(authorsData);
            } 
            else
                return BadRequest("No Data Found");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authorItem = await _unitOfWork.Authors.GetByIdAsync(i=>i.Id==id,new string[] {"News"});
            if (authorItem != null)
            {
                var autherData = new AuthWithNewsDto
                {
                    AuthorName = authorItem.AuthorName,
                    Id = authorItem.Id,
                    Country = authorItem.Country,
                    RelatNews = authorItem.News.Select(i => new AuthRelatNewsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                        NewsContent = i.NewsContent,
                        CreationDate = i.CreationDate,
                        PublicationDate = i.PublicationDate,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                };
                return Ok(autherData);
            }
            else return NoContent();
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByname(string name)
        {
            var authorItem = await _unitOfWork.Authors.GetByIdAsync(i => i.AuthorName.Contains(name) , new string[] { "News" });
            if (authorItem != null)
            {
                var autherData = new AuthWithNewsDto
                {
                    AuthorName = authorItem.AuthorName,
                    Id = authorItem.Id,
                    Country = authorItem.Country,
                    RelatNews = authorItem.News.Select(i => new AuthRelatNewsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                        NewsContent = i.NewsContent,
                        CreationDate = i.CreationDate,
                        PublicationDate = i.PublicationDate,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                };
                return Ok(autherData);
            }
            else return BadRequest("Author Not Found with name:" + name);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Author author)
        {
            if (ModelState.IsValid) 
            {
                var newAuthor = new Author
                {
                    AuthorName = author.AuthorName,
                    Country = author.Country,
                    Biography = author.Biography,
                };
              await _unitOfWork.Authors.AddedAsync(newAuthor);

               return CreatedAtAction(nameof(GetById), new { id = newAuthor.Id },newAuthor);

            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Author author)
        {
            if (ModelState.IsValid) 
            {
              await _unitOfWork.Authors.UpdateAsync(i=>true,author);
              return Ok("Author updated Successfully");
            }
            else return BadRequest(ModelState);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            if (id != null) 
            {
               await _unitOfWork.Authors.DeleteByIdAsync(i => i.Id == id);
              return Ok ("Author deleted Successfully ");
            }else
                return BadRequest(ModelState);
        }

        [HttpGet("GetAuthorOrderBy")]
        public async Task<IActionResult> GetAuthors(string authorNameFilter = null, string orderBy = "Id", string orderByDirection = "ASC")
        {
            Expression<Func<Author, bool>> filter = null;
            if (!string.IsNullOrEmpty(authorNameFilter))
            {
                filter = author => author.AuthorName.Contains(authorNameFilter);
            }

            Expression<Func<Author, object>> orderByExpression = null;
            switch (orderBy.ToLower())
            {
                case "authorname":
                    orderByExpression = author => author.AuthorName;
                    break;
                case "publicationdate":
                    orderByExpression = author => author.Country;
                    break;
                default:
                    orderByExpression = author => author.Id;
                    break;
            }

            var items = await _unitOfWork.Authors.OrderItems(filter, orderByExpression, orderByDirection);

            return Ok(items);
        }




        //[HttpDelete("{name:alpha}")]
        //public async Task<IActionResult> Delete(string authorName)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _unitOfWork.Authors.DeleteByNameAsync(i=>i.AuthorName==authorName);
        //        return Ok("Author deleted Succesfully ");
        //    }
        //    else
        //        return BadRequest(ModelState);
        //}
    }
}
