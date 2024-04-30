using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Author author)
        {
            if (ModelState.IsValid) 
            {
              await _unitOfWork.Authors.UpdateAsync(i=>i.Id==id,author);
              return Ok("Author updated Succesfully");
            }
            else return BadRequest(ModelState);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            if (id != null) 
            {
               await _unitOfWork.Authors.DeleteByIdAsync(i => i.Id == id);
              return Ok ("Author deleted Succesfully ");
            }else
                return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Author author)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Authors.DeleteAsync(author);
                return Ok("Author deleted Succesfully ");
            }
            else
                return BadRequest(ModelState);
        }
    }
}
