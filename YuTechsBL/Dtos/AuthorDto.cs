using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsBL.Dtos
{
    public class AuthorDto
    {
        public string AuthorName { get; set; }
        public string Country { get; set; }
        public string? Biography { get; set; }
    }
}
