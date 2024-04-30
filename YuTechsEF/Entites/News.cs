using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsEF.CustomValidation;
using YuTechsEF.Entity;

namespace YuTechsEF.Entites
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
     
        [Required]
        public string Description { get; set; }
        [Required]
        public string NewsContent { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [ValidPublicationDate]
        public DateTime PublicationDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }= DateTime.Now;


        [ForeignKey("Author")]
        public int AuthorId { get; init; }
        public Author Author { get; set; }


    }
}
