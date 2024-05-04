using Microsoft.AspNetCore.Http;
using YuTechsEF.Entity;

namespace YuTechsBL.Dtos
{
    public class NewsIDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsContent { get; set; }
        public IFormFile ImageFile { get; set; } // New property for image file
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
        
    }

}
