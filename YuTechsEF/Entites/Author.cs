using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsEF.Entites;

namespace YuTechsEF.Entity
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(20, MinimumLength = 3, ErrorMessage = "Author's name must be between 3 and 20 characters")]
        public string AuthorName { get; set; }
        [Required]
        public string Country { get; set; }
        public string ?Biography { get; set; }

        public IEnumerable<News>?News { get; set; }=new List<News>();
    }
}
