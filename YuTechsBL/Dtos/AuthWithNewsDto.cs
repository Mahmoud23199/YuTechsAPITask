using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsBL.Dtos
{
    public class AuthWithNewsDto
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Country { get; set; }

        public string? Biography { get; set; }


        public List<AuthRelatNewsDto> RelatNews { get; set; }
    }
}
